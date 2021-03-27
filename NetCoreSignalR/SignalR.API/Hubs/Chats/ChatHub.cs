using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.API.Hubs.Chats
{
    public class ChatHub : Hub<IChatHub>
    {
        private static List<string> OnlineUserConnectionId { get; set; } = new List<string>();

        public async override Task OnConnectedAsync()
        {
            OnlineUserConnectionId.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            OnlineUserConnectionId.Remove(Context.ConnectionId);
            await GetConnectionIdsAsync();
            await base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessageAsync(string message, string connectionId)
        {
            await Clients.Client(connectionId).ReceiveMessage(message);
        }

        public async Task GetConnectionIdsAsync()
        {
            await Clients.All.ReceiveConnectionIds(OnlineUserConnectionId);
        }
    }
}
