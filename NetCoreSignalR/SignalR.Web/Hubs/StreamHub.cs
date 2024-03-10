using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.Web.Hubs
{
    public interface IStreamHub
    {
        Task ReceiveMessageAsStreamForAllClient(string name);
    }

    public class StreamHub : Hub<IStreamHub>
    {
        public async Task BroadcastStreamDataToAllClient(IAsyncEnumerable<string> nameAsChunks)
        {
            await foreach (var name in nameAsChunks)
            {
                await Task.Delay(1000); //Yüklü miktarda data örneği yaratmak amacıyla 1 sn gecikme eklendi.
                await Clients.All.ReceiveMessageAsStreamForAllClient(name);
            }
        }

        public 
    }
}
