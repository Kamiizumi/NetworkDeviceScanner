using Kamiizumi.NetworkDeviceScanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Kamiizumi.NetworkDeviceScanner.Data
{
    public class NetworkDeviceScannerContext : DbContext
    {
        public NetworkDeviceScannerContext(DbContextOptions<NetworkDeviceScannerContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
    }
}
