namespace Kamiizumi.NetworkDeviceScanner.Web.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Kamiizumi.NetworkDeviceScanner.Data;
    using Kamiizumi.NetworkDeviceScanner.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using NmapXmlParser;

    /// <summary>
    /// Service for discovering devices on a network.
    /// </summary>
    public class DeviceScannerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly NetworkDeviceScannerOptions _networkDeviceScannerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceScannerService"/> class.
        /// </summary>
        /// <param name="scopeFactory">Factory for getting services.</param>
        /// <param name="options">Application options.</param>
        public DeviceScannerService(IServiceScopeFactory scopeFactory, IOptions<NetworkDeviceScannerOptions> options)
        {
            _scopeFactory = scopeFactory;
            _networkDeviceScannerOptions = options.Value;
        }

        /// <summary>
        /// Executes the background service.
        /// </summary>
        /// <param name="stoppingToken">Token to check if the service has been asked to stop.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var nextRunTime = DateTime.Now;

            while (stoppingToken.IsCancellationRequested == false)
            {
                if (DateTime.Now >= nextRunTime)
                {
                    using (var nmapProcess = new Process())
                    {
                        // Configure the executable / arguments to start.
                        nmapProcess.StartInfo.FileName = "nmap";
                        nmapProcess.StartInfo.Arguments = $"-sn {_networkDeviceScannerOptions.TargetSpecification} -oX -";

                        // We want to handle the process internally so don't start via the OS.
                        nmapProcess.StartInfo.UseShellExecute = false;

                        // Redirect output so we can process readings / errors.
                        nmapProcess.StartInfo.RedirectStandardOutput = true;
                        nmapProcess.StartInfo.RedirectStandardError = false;

                        // Begin the process and start listening to output.
                        Console.WriteLine("Starting nmap process...");
                        nmapProcess.Start();

                        var xml = await nmapProcess.StandardOutput.ReadToEndAsync();

                        var xmlSerializer = new XmlSerializer(typeof(nmaprun));
                        var result = default(nmaprun);

                        using (var xmlStream = new StringReader(xml))
                        {
                            result = xmlSerializer.Deserialize(xmlStream) as nmaprun;
                        }

                        var discoveredHosts = result.Items.OfType<host>().Where(a => a.Items.OfType<address>().Any()).Select(host => new Device()
                        {
                            MacAddress = host.Items.OfType<address>().Single().addr.Replace(":", string.Empty),
                            LastSeenIp = host.address.addr,
                            LastSeenHostName = host.Items.OfType<hostnames>().Single().hostname?.FirstOrDefault()?.name,
                            LastSeenAt = DateTime.Now,
                        });

                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var context = scope.ServiceProvider.GetRequiredService<NetworkDeviceScannerContext>();

                            foreach (var discoveredHost in discoveredHosts)
                            {
                                var existingHost = await context.Devices.FindAsync(discoveredHost.MacAddress);

                                if (existingHost != null)
                                {
                                    existingHost.LastSeenIp = discoveredHost.LastSeenIp;
                                    existingHost.LastSeenHostName = discoveredHost.LastSeenHostName;
                                    existingHost.LastSeenAt = discoveredHost.LastSeenAt;
                                }
                                else
                                {
                                    context.Devices.Add(discoveredHost);
                                }
                            }

                            context.SaveChanges();
                        }

                        nextRunTime = DateTime.Now + TimeSpan.FromSeconds(30);
                        Console.WriteLine($"Done. Next run at: {nextRunTime}");
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
