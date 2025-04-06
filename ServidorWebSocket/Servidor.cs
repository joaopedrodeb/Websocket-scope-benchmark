using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ServidorWebSocket
{
    public static class Servidor
    {
        public static int TotalMensagensRecebidas = 0;

        public static async Task StartAsync(CancellationToken cancellationToken)
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.UseWebSockets();

            app.Map("/ws", async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    using var ws = await context.WebSockets.AcceptWebSocketAsync();
                    var buffer = new byte[1024 * 4];

                    while (!cancellationToken.IsCancellationRequested && ws.State == WebSocketState.Open)
                    {
                        try
                        {
                            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                            if (result.MessageType == WebSocketMessageType.Close)
                                break;

                            if (result.Count > 0)
                            {
                                Interlocked.Increment(ref TotalMensagensRecebidas);

                                var timestamp = DateTime.UtcNow.ToString("o"); // ISO 8601
                                var datagrama = $"{{\"status\":\"ack\",\"timestamp\":\"{timestamp}\",\"idCliente\":\"{Guid.NewGuid()}\",\"mensagem\":\"recebido\"}}";
                                var resposta = Encoding.UTF8.GetBytes(datagrama);


                                await ws.SendAsync(new ArraySegment<byte>(resposta), WebSocketMessageType.Text, true, cancellationToken);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao receber mensagem: {ex.Message}");
                            break;
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });

            app.Urls.Add("http://localhost:5000");
            await app.RunAsync(cancellationToken);
        }
    }
}
