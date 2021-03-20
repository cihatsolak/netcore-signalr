using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR.API.Hubs;

namespace SignalR.API
{
    public class Startup
    {
        // Bu y�ntem �al��ma zaman� taraf�ndan �a�r�l�r. Kapsay�c�ya hizmet eklemek i�in bu y�ntemi kullan�n.
        // Uygulaman�z� nas�l yap�land�raca��n�zla ilgili daha fazla bilgi i�in https://go.microsoft.com/fwlink/?LinkID=398940 adresini ziyaret edin.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(); //SignalR ekliyorum.
        }

        // Bu y�ntem �al��ma zaman� taraf�ndan �a�r�l�r.HTTP istek ard���k d�zenini yap�land�rmak i�in bu y�ntemi kullan�n.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                /* Clientlar benim signalr hub'�ma MyHub �zerinden ba�lans�n
                 */
                endpoints.MapHub<MyHub>("/MyHub");
            });
        }
    }
}
