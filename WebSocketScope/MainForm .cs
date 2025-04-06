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
        private string _csvPath = "log_teste.csv";

        public MainForm()
        {
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

            // Grava o cabeçalho no CSV
            using (var writer = new StreamWriter(_csvPath, append: false))
            {
                writer.WriteLine("Hora       | Conexões Ativas | Memória (MB)");
            }

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

                    Invoke(() => listBoxLog.Items.Insert(1, log)); // Insere abaixo do cabeçalho
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
        }
    }
}
