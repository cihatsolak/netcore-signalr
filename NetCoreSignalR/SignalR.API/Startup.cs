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
        // Bu yöntem çalýþma zamaný tarafýndan çaðrýlýr. Kapsayýcýya hizmet eklemek için bu yöntemi kullanýn.
        // Uygulamanýzý nasýl yapýlandýracaðýnýzla ilgili daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkID=398940 adresini ziyaret edin.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(); //SignalR ekliyorum.
        }

        // Bu yöntem çalýþma zamaný tarafýndan çaðrýlýr.HTTP istek ardýþýk düzenini yapýlandýrmak için bu yöntemi kullanýn.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                /* Clientlar benim signalr hub'ýma MyHub üzerinden baðlansýn
                 */
                endpoints.MapHub<MyHub>("/MyHub");
            });
        }
    }
}
