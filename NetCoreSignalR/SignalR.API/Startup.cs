using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SignalR.API.Hubs;

namespace SignalR.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:2500") //Bu url adresine izin ver.
                    .AllowAnyHeader() //headerý kabul et.
                    .AllowAnyMethod() //tüm metotlar için
                    .AllowCredentials();
                });
            });

            services.AddControllers();
            services.AddSignalR(); //SignalR ekliyorum.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                /* Clientlar benim signalr hub'ýma MyHub üzerinden baðlansýn
                 */
                endpoints.MapHub<MyHub>("/MyHub");
                endpoints.MapHub<FootballHub>("/FootballHub");
            });
        }
    }
}
