namespace Kamiizumi.NetworkDeviceScanner.Web
{
    using Kamiizumi.NetworkDeviceScanner.Data;
    using Kamiizumi.NetworkDeviceScanner.Services;
    using Kamiizumi.NetworkDeviceScanner.Web.Components;
    using Kamiizumi.NetworkDeviceScanner.Web.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Initialises and configures the web application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services used throughout the application.
        /// </summary>
        /// <param name="services">Collection of configured services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<NetworkDeviceScannerOptions>(Configuration);

            services.AddDbContext<NetworkDeviceScannerContext>(options =>
                options.UseSqlite("Data Source=Data/NetworkDeviceScanner.db"));

            services.AddRazorPages();
            services.AddServerSideBlazor();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
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
