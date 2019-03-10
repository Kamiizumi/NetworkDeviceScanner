using Kamiizumi.NetworkDeviceScanner.Data;
using Kamiizumi.NetworkDeviceScanner.Data.Models;
using System;
using System.Threading.Tasks;

namespace Kamiizumi.NetworkDeviceScanner.Services
{
    public class ProfileService
    {
        private readonly NetworkDeviceScannerContext _networkDeviceScannerContext;

        public ProfileService(NetworkDeviceScannerContext networkDeviceScannerContext)
        {
            _networkDeviceScannerContext = networkDeviceScannerContext;
        }

        public async Task<Profile> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var profile = new Profile
            {
                Name = name
            };

            await _networkDeviceScannerContext.Profiles.AddAsync(profile);
            await _networkDeviceScannerContext.SaveChangesAsync();

            return profile;
        }
    }
}
