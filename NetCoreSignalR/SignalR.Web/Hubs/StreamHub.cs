using Microsoft.AspNetCore.SignalR;
using SignalR.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Web.Hubs
{
    public interface IStreamHub
    {
        Task ReceiveMessageAsStreamForAllClient(string name);
        Task ReceiveProductAsStreamForAllClient(Product product);
    }

    public class StreamHub : Hub<IStreamHub>
    {
        public async Task BroadcastStreamDataToAllClient(IAsyncEnumerable<string> nameAsChunks)
        {
            await foreach (var name in nameAsChunks)
            {
                await Task.Delay(TimeSpan.FromSeconds(1)); //Yüklü miktarda data örneği yaratmak amacıyla 1 sn gecikme eklendi.
                await Clients.All.ReceiveMessageAsStreamForAllClient(name);
            }
        }

        public async Task BroadcastStreamProductsToAllClient(IAsyncEnumerable<Product> productAsChunks)
        {
            await foreach (var product in productAsChunks)
            {
                await Task.Delay(TimeSpan.FromSeconds(1)); //Yüklü miktarda data örneği yaratmak amacıyla 1 sn gecikme eklendi.
                await Clients.All.ReceiveProductAsStreamForAllClient(product);
            }
        }

        public async IAsyncEnumerable<string> BroadCastFromHubToClient(int capacity)
        {
            List<int> numbers = Enumerable.Range(1, capacity).ToList();

            foreach (var number in numbers)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                yield return $"{number}. number";
            }
        } 
    }
}
