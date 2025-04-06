using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ServidorWebSocket
{
    public static class Servidor
    {
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
                    var buffer = new byte[1024];

                    while (!cancellationToken.IsCancellationRequested && ws.State == WebSocketState.Open)
                    {
                        var result = await ws.ReceiveAsync(buffer, cancellationToken);
                        if (result.MessageType == WebSocketMessageType.Close)
                            break;
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
