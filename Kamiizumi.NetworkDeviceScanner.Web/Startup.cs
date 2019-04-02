namespace Kamiizumi.NetworkDeviceScanner.Web
{
    using Kamiizumi.NetworkDeviceScanner.Data;
    using Kamiizumi.NetworkDeviceScanner.Services;
    using Kamiizumi.NetworkDeviceScanner.Web.Components;
    using Kamiizumi.NetworkDeviceScanner.Web.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Initialises and configures the web application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures services used throught the application.
        /// </summary>
        /// <param name="services">Collection of configured services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </remarks>
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

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</remarks>
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
