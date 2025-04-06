using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ServidorWebSocket;
using WebSocketScope.Servicos;

namespace WebSocketScope
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cts = new();
        private int _conexoesAtivas = 0;
        private string _logFolder;
        private string _csvPath;

        public MainForm()
        {
            _logFolder = Path.Combine("logs", $"log_{DateTime.Now:yyyyMMdd_HHmmss}");
            _csvPath = Path.Combine(_logFolder, "log.csv");

            CsvExporter.CriarCsv(_csvPath, "Hora       | Conexões Ativas | Memória (MB)");

            InitializeComponent();
            btnParar.Enabled = false;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            btnIniciar.Enabled = false;
            btnParar.Enabled = true;
            _cts = new CancellationTokenSource();
            _conexoesAtivas = 0;

            int total = (int)numericConexoes.Value;
            int tempoSegundos = (int)numericSegundos.Value;

            // Exibe o cabeçalho na ListBox
            Invoke(() =>
            {
                listBoxLog.Items.Clear();
                listBoxLog.Items.Add("Hora       | Conexões Ativas | Memória (MB)");
            });

            // Inicia o servidor WebSocket em segundo plano
            _ = Task.Run(() => Servidor.StartAsync(_cts.Token));

            // Cria as conexões simuladas
            for (int i = 0; i < total; i++)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        using var ws = new ClientWebSocket();
                        await ws.ConnectAsync(new Uri("ws://localhost:5000/ws"), _cts.Token);
                        Interlocked.Increment(ref _conexoesAtivas);

                        var buffer = Encoding.UTF8.GetBytes("ping");
                        await ws.SendAsync(buffer, WebSocketMessageType.Text, true, _cts.Token);

                        await Task.Delay(tempoSegundos * 1000, _cts.Token);
                    }
                    catch
                    {
                        // Ignora erros de conexão
                    }
                    finally
                    {
                        Interlocked.Decrement(ref _conexoesAtivas);
                    }
                });
            }

            // Tarefa para monitoramento em tempo real
            _ = Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    long mem = GC.GetTotalMemory(false);
                    double mb = mem / 1024.0 / 1024.0;

                    string log = BenchmarkService.GerarLog(DateTime.Now, _conexoesAtivas, mb);

                    Invoke(() =>
                    {
                        if (listBoxLog.Items.Count > 1000)
                            listBoxLog.Items.RemoveAt(listBoxLog.Items.Count - 1);

                        listBoxLog.Items.Insert(1, log);
                    });

                    CsvExporter.AdicionarLinha(_csvPath, log);
                    await Task.Delay(1000);
                }
            });
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
            btnIniciar.Enabled = true;
            btnParar.Enabled = false;
        }
    }
}
