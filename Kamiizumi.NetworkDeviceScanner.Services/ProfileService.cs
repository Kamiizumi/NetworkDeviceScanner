namespace Kamiizumi.NetworkDeviceScanner.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Kamiizumi.NetworkDeviceScanner.Data;
    using Kamiizumi.NetworkDeviceScanner.Data.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Service for working with <see cref="Profile"/> entities.
    /// </summary>
    public class ProfileService
    {
        private readonly NetworkDeviceScannerContext _networkDeviceScannerContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService"/> class.
        /// </summary>
        /// <param name="networkDeviceScannerContext">Context to access entities with.</param>
        public ProfileService(NetworkDeviceScannerContext networkDeviceScannerContext)
        {
            _networkDeviceScannerContext = networkDeviceScannerContext;
        }

        /// <summary>
        /// Creates a new profile.
        /// </summary>
        /// <param name="name">Name for the profile.</param>
        /// <returns>Created profile.</returns>
        public async Task<Profile> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var profile = new Profile
            {
                Name = name,
            };

            await _networkDeviceScannerContext.Profiles.AddAsync(profile);
            await _networkDeviceScannerContext.SaveChangesAsync();

            return profile;
        }

        /// <summary>
        /// Gets a queryable of all profiles.
        /// </summary>
        /// <returns>Queryable of profiles.</returns>
        public IQueryable<Profile> Get()
        {
            return _networkDeviceScannerContext.Profiles;
        }

        /// <summary>
        /// Deletes a profile, also removing the profile from any assigned devices.
        /// </summary>
        /// <param name="profileId">Database ID of the profile to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
