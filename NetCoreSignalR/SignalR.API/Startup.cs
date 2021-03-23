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
                    .AllowAnyHeader() //header� kabul et.
                    .AllowAnyMethod() //t�m metotlar i�in
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

                /* Clientlar benim signalr hub'�ma MyHub �zerinden ba�lans�n
                 */
                endpoints.MapHub<MyHub>("/MyHub");
                endpoints.MapHub<FootballHub>("/FootballHub");
            });
        }
    }
}
