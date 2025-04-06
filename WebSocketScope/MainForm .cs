using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ServidorWebSocket;
using WebSocketScope.Servicos;
using System.Text.Json;
using System.Collections.Concurrent;

namespace WebSocketScope
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cts = new();
        private int _conexoesAtivas = 0;
        private int _mensagensAtivas = 0;
        private string _logFolder;
        private string _csvPath;
        private SemaphoreSlim _limitePingPong = new(5);
        private SemaphoreSlim _limiteConexoes = new(50); // NOVO limite de conexões simultâneas
        private ConcurrentDictionary<int, string> _ultimosAcks = new();

        public MainForm()
        {
            _logFolder = Path.Combine("logs", $"log_{DateTime.Now:yyyyMMdd_HHmmss}");
            Directory.CreateDirectory(_logFolder);
            _csvPath = Path.Combine(_logFolder, "log.csv");

            InitializeComponent();
            btnParar.Enabled = false;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            btnIniciar.Enabled = false;
            btnParar.Enabled = true;
            _cts = new CancellationTokenSource();
            Interlocked.Exchange(ref _conexoesAtivas, 0);
            Interlocked.Exchange(ref _mensagensAtivas, 0);
            _ultimosAcks.Clear();

            int total = (int)numericConexoes.Value;
            int tempoSegundos = (int)numericSegundos.Value;

            CriarCabecalhoCsv();
            InicializarInterfaceGrafica();
            IniciarServidorWebSocket();
            CriarConexoesSimuladas(total, tempoSegundos);
            IniciarMonitoramentoMemoria();
        }

        private void CriarCabecalhoCsv()
        {
            using var writer = new StreamWriter(_csvPath, append: false);
            writer.WriteLine("Hora       | Conexões Ativas | PingPong Ativos | Memória (MB) | Último Ack");
        }

        private void InicializarInterfaceGrafica()
        {
            Invoke(() =>
            {
                listBoxLog.Items.Clear();
                listBoxLog.Items.Add("Hora       | Conexões Ativas | PingPong Ativos | Memória (MB) | Último Ack");
            });
        }

        private void IniciarServidorWebSocket()
        {
            _ = Task.Run(async () =>
            {
                await Servidor.StartAsync(_cts.Token);
            });

            Thread.Sleep(1000);
        }

        private void CriarConexoesSimuladas(int total, int tempoSegundos)
        {
            for (int i = 0; i < total; i++)
            {
                int clientIdCapturado = i;

                _ = Task.Run(async () =>
                {
                    await _limiteConexoes.WaitAsync();
                    int mensagensRecebidas = 0;
                    bool conectado = false;

                    try
                    {
                        using var ws = new ClientWebSocket();
                        await ws.ConnectAsync(new Uri("ws://localhost:5000/ws"), _cts.Token);

                        conectado = true;
                        Interlocked.Increment(ref _conexoesAtivas);

                        await Task.Delay(100);

                        var bufferEnvio = Encoding.UTF8.GetBytes("ping");
                        var bufferRecepcao = new byte[1024];
                        var tempoLimite = DateTime.Now.AddSeconds(tempoSegundos);

                        while (DateTime.Now < tempoLimite && !_cts.Token.IsCancellationRequested)
                        {
                            await _limitePingPong.WaitAsync();
                            Interlocked.Increment(ref _mensagensAtivas);
                            try
                            {
                                if (ws.State == WebSocketState.Open)
                                {
                                    await ws.SendAsync(new ArraySegment<byte>(bufferEnvio), WebSocketMessageType.Text, true, _cts.Token);
                                    var result = await ws.ReceiveAsync(new ArraySegment<byte>(bufferRecepcao), _cts.Token);

                                    if (result.MessageType == WebSocketMessageType.Close)
                                        break;

                                    if (result.Count > 0)
                                    {
                                        mensagensRecebidas++;

                                        string resposta = Encoding.UTF8.GetString(bufferRecepcao, 0, result.Count);

                                        try
                                        {
                                            using JsonDocument doc = JsonDocument.Parse(resposta);
                                            string status = doc.RootElement.GetProperty("status").GetString() ?? "erro";
                                            string timestamp = doc.RootElement.GetProperty("timestamp").GetString() ?? "?";
                                            string idCliente = doc.RootElement.GetProperty("idCliente").GetString() ?? "?";
                                            string texto = $"Cliente {clientIdCapturado} → {status} | {DateTime.Parse(timestamp):HH:mm:ss} | {idCliente[^6..]}";
                                            _ultimosAcks[clientIdCapturado] = texto;
                                        }
                                        catch
                                        {
                                            _ultimosAcks[clientIdCapturado] = $"Cliente {clientIdCapturado} → inválido";
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _ultimosAcks[clientIdCapturado] = $"Erro cliente {clientIdCapturado}: {ex.Message}";
                                break;
                            }
                            finally
                            {
                                _limitePingPong.Release();
                                Interlocked.Decrement(ref _mensagensAtivas);
                            }

                            await Task.Delay(5000);
                        }

                        if (ws.State == WebSocketState.Open)
                        {
                            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Encerrando", CancellationToken.None);
                        }
                    }
                    catch (Exception ex)
                    {
                        Invoke(() => listBoxLog.Items.Add($"Cliente {clientIdCapturado} falhou: {ex.Message}"));
                    }
                    finally
                    {
                        if (conectado)
                            Interlocked.Decrement(ref _conexoesAtivas);

                        if (mensagensRecebidas > 0)
                        {
                            Invoke(() =>
                            {
                                string log = $"Cliente {clientIdCapturado} recebeu {mensagensRecebidas} mensagens.";
                                listBoxLog.Items.Add(log);
                            });
                        }

                        _limiteConexoes.Release();
                    }
                });
            }
        }

        private void IniciarMonitoramentoMemoria()
        {
            _ = Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    long mem = GC.GetTotalMemory(false);
                    double mb = mem / 1024.0 / 1024.0;
                    int conexoes = Interlocked.CompareExchange(ref _conexoesAtivas, 0, 0);
                    int mensagens = Interlocked.CompareExchange(ref _mensagensAtivas, 0, 0);

                    string ultimoAck = _ultimosAcks.Count > 0 ? _ultimosAcks.Values.LastOrDefault() ?? "-" : "-";
                    string log = $"{DateTime.Now:HH:mm:ss} | {conexoes,4} | {mensagens,6} | {mb:0.00} MB | {ultimoAck}";

                    Invoke(() =>
                    {
                        if (listBoxLog.Items.Count > 1000)
                            listBoxLog.Items.RemoveAt(listBoxLog.Items.Count - 1);

                        listBoxLog.Items.Insert(1, log);
                    });

                    await File.AppendAllTextAsync(_csvPath, log + "\n");
                    await Task.Delay(1000);
                }
            });
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
            btnIniciar.Enabled = true;
            btnParar.Enabled = false;

            if (!string.IsNullOrWhiteSpace(txtEmail?.Text))
            {
                EmailService.EnviarRelatorioComHtml(txtEmail.Text.Trim(), _csvPath);
                listBoxLog.Items.Add($"Relatório enviado para: {txtEmail.Text.Trim()}");
            }
        }

        private void btnConfigurarEmail_Click(object sender, EventArgs e)
        {
            using var form = new EmailConfigForm();
            form.ShowDialog();
        }

    }
}
