using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.API.Hubs.Chats
{
    public interface IChatHub
    {
        Task ReceiveMessage(string message);
        Task ReceiveConnectionIds(List<string> connectionIds);
    }
}
