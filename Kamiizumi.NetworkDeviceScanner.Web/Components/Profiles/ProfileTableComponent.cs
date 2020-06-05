namespace Kamiizumi.NetworkDeviceScanner.Web.Components.Profiles
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Kamiizumi.NetworkDeviceScanner.Services;
    using Microsoft.AspNetCore.Components;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Component showing all existing profiles.
    /// </summary>
    public class ProfileTableComponent : ComponentBase
    {
        /// <summary>
        /// Gets or sets the service to access profiles with.
        /// </summary>
        [Inject]
        protected ProfileService ProfileService { get; set; }

        /// <summary>
        /// Gets or sets a list of view models containing profiles.
        /// </summary>
        protected IEnumerable<ProfileTableVm> ProfileTableVms { get; set; }

        /// <summary>
        /// Prepares the component.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            ProfileTableVms = await ProfileService
                .Get()
                .Select(profile => new ProfileTableVm
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    DeviceCount = profile.Devices.Count,
                })
                .OrderBy(profileTableVm => profileTableVm.Name)
                .ToListAsync();
        }
    }
}
