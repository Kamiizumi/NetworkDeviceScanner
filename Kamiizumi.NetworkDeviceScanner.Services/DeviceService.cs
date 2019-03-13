using Kamiizumi.NetworkDeviceScanner.Data;
using System.Threading.Tasks;

namespace Kamiizumi.NetworkDeviceScanner.Services
{
    public class DeviceService
    {
        private readonly NetworkDeviceScannerContext _context;

        public DeviceService(NetworkDeviceScannerContext context)
        {
            _context = context;
        }

        public async Task SetProfile(string macAddress, int profileId)
        {
            var device = await _context.Devices.FindAsync(macAddress);
            var profile = await _context.Profiles.FindAsync(profileId);

            device.Profile = profile;

            await _context.SaveChangesAsync();
        }
    }
}
