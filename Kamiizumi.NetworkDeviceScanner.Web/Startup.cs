using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kamiizumi.NetworkDeviceScanner.Web.Components;
using Kamiizumi.NetworkDeviceScanner.Web.Services;
using Kamiizumi.NetworkDeviceScanner.Data;
using Microsoft.EntityFrameworkCore;
using Kamiizumi.NetworkDeviceScanner.Services;

namespace Kamiizumi.NetworkDeviceScanner.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NetworkDeviceScannerContext>(options =>
                options.UseSqlite("Data Source=NetworkDeviceScanner.db"));

            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddRazorComponents();

            services.AddScoped<ProfileService>();
            services.AddScoped<DeviceService>();

            services.AddSingleton<IHostedService, DeviceScannerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting(routes =>
            {
                routes.MapRazorPages();
                routes.MapComponentHub<App>("app");
            });

            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<NetworkDeviceScannerContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
