using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kamiizumi.NetworkDeviceScanner.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Kamiizumi.NetworkDeviceScanner.Web.Components.Profiles
{
    public class ProfileTableComponent : ComponentBase
    {
        [Inject]
        protected ProfileService ProfileService { get; set; }

        protected IEnumerable<ProfileTableVm> _profileTableVms;

        protected override async Task OnInitAsync()
        {
            _profileTableVms = await ProfileService
                .Get()
                .Select(profile => new ProfileTableVm
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    DeviceCount = profile.Devices.Count
                })
                .OrderBy(profileTableVm => profileTableVm.Name)
                .ToListAsync();
        }
    }
}
