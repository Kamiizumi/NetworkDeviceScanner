namespace Kamiizumi.NetworkDeviceScanner.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Kamiizumi.NetworkDeviceScanner.Data;
    using Kamiizumi.NetworkDeviceScanner.Data.Models;

    /// <summary>
    /// Service for working with <see cref="Device"/> entities.
    /// </summary>
    public class DeviceService
    {
        private readonly NetworkDeviceScannerContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class.
        /// </summary>
        /// <param name="context">Context to access entities with.</param>
        public DeviceService(NetworkDeviceScannerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a queryable of all devices.
        /// </summary>
        /// <returns>Queryable of devices.</returns>
        public IQueryable<Device> Get()
        {
            return _context.Devices;
        }

        /// <summary>
        /// Sets the profile a device should be assigned to.
        /// </summary>
        /// <param name="macAddress">MAC address of the device to update.</param>
        /// <param name="profileId">Database ID of the profile to assign the device to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SetProfile(string macAddress, int profileId)
        {
            var device = await _context.Devices.FindAsync(macAddress);
            var profile = await _context.Profiles.FindAsync(profileId);

            device.Profile = profile;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Sets the user defined name of a device.
        /// </summary>
        /// <param name="macAddress">MAC address of the device to update.</param>
        /// <param name="userDefinedName">User defined name to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SetUserDefinedName(string macAddress, string userDefinedName)
        {
            var device = await _context.Devices.FindAsync(macAddress);

            if (string.IsNullOrWhiteSpace(userDefinedName))
            {
                device.UserDefinedName = null;
            }
            else
            {
                device.UserDefinedName = userDefinedName.Trim();
            }

            await _context.SaveChangesAsync();
        }
    }
}
