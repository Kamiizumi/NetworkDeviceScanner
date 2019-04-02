namespace Kamiizumi.NetworkDeviceScanner.Data
{
    using Kamiizumi.NetworkDeviceScanner.Data.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Database context for the application.
    /// </summary>
    public class NetworkDeviceScannerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkDeviceScannerContext"/> class.
        /// </summary>
        /// <param name="options">Options to connect to the database with.</param>
        public NetworkDeviceScannerContext(DbContextOptions<NetworkDeviceScannerContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the devices that are being tracked by the context.
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Gets or sets the profiles that are being tracked by the context.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Defines additional context and entity model configuration.
        /// </summary>
        /// <param name="modelBuilder">Builder to configure.</param>
        /// <remarks>
        /// Annotations should be preferred over this method.
        /// The fluent API should be used only if a Annotation equivalant does not exist.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure profile names are unique.
            modelBuilder
                .Entity<Profile>()
                .HasAlternateKey(profile => profile.Name);
        }
    }
}
