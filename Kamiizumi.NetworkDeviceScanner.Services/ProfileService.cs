using Kamiizumi.NetworkDeviceScanner.Data;
using Kamiizumi.NetworkDeviceScanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public IQueryable<Profile> Get()
        {
            return _networkDeviceScannerContext.Profiles;
        }

        public async Task Delete(int profileId)
        {
            var profileDeleting = await _networkDeviceScannerContext
                .Profiles
                .Include(profile => profile.Devices)
                .FirstOrDefaultAsync(profile => profile.Id == profileId);

            if (profileDeleting == null)
            {
                throw new ArgumentException("Unable to find profile.", nameof(profileId));
            }

            profileDeleting.Devices.Clear();
            _networkDeviceScannerContext.Remove(profileDeleting);
            await _networkDeviceScannerContext.SaveChangesAsync();
        }
    }
}
