using SignalR.API.Models;
using System.Threading.Tasks;

namespace SignalR.API.Hubs.Products
{
    /// <summary>
    /// Clientlarda çalışacak olan metotlarımızı yazıyoruz.
    /// </summary>
    public interface IProductHub //Örneğin productlarla çalışan realtime uygulamamız var.
    {
        Task ReceiveProduct(Product product);
    }
}
