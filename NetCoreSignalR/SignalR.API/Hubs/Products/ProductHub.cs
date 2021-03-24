using Microsoft.AspNetCore.SignalR;
using SignalR.API.Models;
using System.Threading.Tasks;

namespace SignalR.API.Hubs.Products
{
    /// <summary>
    /// Strongly typed Hubs
    /// Buradaki kullanım amacı hata yapma payımızı azaltmaktır.
    /// </summary>
    public class ProductHub : Hub<IProductHub>
    {
        //Clientlar "SendProductAsync" metotunu invoke edecek.
        public async Task SendProductAsync(Product product)
        {
            /*
             * ReceiveProduct: Clientlar bu metot ismine abone olacak.
             */
            await Clients.All.ReceiveProduct(product); 
        }
    }
}
