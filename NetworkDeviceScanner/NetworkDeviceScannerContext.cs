using Microsoft.EntityFrameworkCore;
using NetworkDeviceScanner.Models;

namespace NetworkDeviceScanner
{
    class NetworkDeviceScannerContext : DbContext
    {
        public DbSet<DiscoveredDevice> DiscoveredDevices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=NetworkDeviceScanner.db");
        }
    }
}
