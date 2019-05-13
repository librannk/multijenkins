using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using TransactionQueue.SocketManager;


namespace TransactionQueue.SocketManager.Handlers
{
    public class TQMessageHandler : WebSocketHandler
    {
        public TQMessageHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }
}