using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWebSocket
{
    class Program
    {
        static Encoding encoding = UTF8Encoding.UTF8;

        static async Task Main()
        {
            using (var webSocket = new ClientWebSocket())
            {
                var serverUri = new Uri("wss://echo.websocket.org/");
                await webSocket.ConnectAsync(serverUri, CancellationToken.None);

                var rcvBuff = new byte[32];
                var rcvTask = webSocket.ReceiveAsync(rcvBuff, CancellationToken.None);

                var sndText = "Hello, WebSocket!";
                var sndBuff = encoding.GetBytes(sndText);
                await webSocket.SendAsync(sndBuff, WebSocketMessageType.Text, true, CancellationToken.None);

                var rcvMessage = await rcvTask;
                var rcvText = encoding.GetString(rcvBuff, 0, rcvMessage.Count);
                Console.WriteLine($"Received Text: \"{rcvText}\".");
            }
        }
    }
}
