using Microsoft.EntityFrameworkCore;
using NetworkDeviceScanner.Models;
using NmapXmlParser;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace NetworkDeviceScanner
{
    class Program
    {
        private static bool _keepRunning = true;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            var contextb = new NetworkDeviceScannerContext();
            contextb.Database.Migrate();

            var nextRunTime = DateTime.Now;

            while(_keepRunning)
            {
                if(DateTime.Now >= nextRunTime)
                {
                    using (var nmapProcess = new Process())
                    {
                        // Configure the executable / arguments to start.
                        nmapProcess.StartInfo.FileName = "nmap";
                        nmapProcess.StartInfo.Arguments = "-sn 192.168.0.0/24 -oX -";

                        // We want to handle the process internally so don't start via the OS.
                        nmapProcess.StartInfo.UseShellExecute = false;

                        // Redirect output so we can process readings / errors.
                        nmapProcess.StartInfo.RedirectStandardOutput = true;
                        nmapProcess.StartInfo.RedirectStandardError = false;

                        // Begin the process and start listening to output.
                        Console.WriteLine("Starting nmap process...");
                        nmapProcess.Start();

                        var xml = nmapProcess.StandardOutput.ReadToEnd();

                        var xmlSerializer = new XmlSerializer(typeof(nmaprun));
                        var result = default(nmaprun);

                        using (var xmlStream = new StringReader(xml))
                        {
                            result = xmlSerializer.Deserialize(xmlStream) as nmaprun;
                        }

                        var newHosts = result.Items.OfType<host>().Where(a => a.Items.OfType<address>().Any()).Select(host => new DiscoveredDevice()
                        {
                            MacAddress = host.Items.OfType<address>().Single().addr.Replace(":", ""),
                            LastSeen = DateTime.Now
                        });

                        var context = new NetworkDeviceScannerContext();

                        foreach (var newHost in newHosts)
                        {
                            if (context.DiscoveredDevices.AsNoTracking().Any(a => a.MacAddress == newHost.MacAddress))
                            {
                                context.DiscoveredDevices.Update(newHost);
                            }
                            else
                            {
                                context.DiscoveredDevices.Add(newHost);
                            }
                        }

                        context.SaveChanges();

                        nextRunTime = DateTime.Now + TimeSpan.FromSeconds(30);
                        Console.WriteLine($"Done. Next run at: {nextRunTime}");
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Stopping...");
            e.Cancel = true;
            _keepRunning = false;
        }
    }
}
