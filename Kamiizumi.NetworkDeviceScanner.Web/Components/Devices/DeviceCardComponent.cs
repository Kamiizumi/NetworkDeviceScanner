namespace Kamiizumi.NetworkDeviceScanner.Web.Components.Devices
{
    using System.Linq;
    using System.Threading.Tasks;
    using Kamiizumi.NetworkDeviceScanner.Services;
    using Microsoft.AspNetCore.Components;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Component showing details of a device on a card.
    /// </summary>
    public class DeviceCardComponent : ComponentBase
    {
        /// <summary>
        /// Gets or sets the service to access devices with.
        /// </summary>
        [Inject]
        protected DeviceService DeviceService { get; set; }

        /// <summary>
        /// Gets the view model containing device details.
        /// </summary>
        protected DeviceCardVm DeviceCardVm { get; private set; }

        /// <summary>
        /// Gets or sets the MAC address of the device the card is for.
        /// </summary>
        [Parameter]
        protected string MacAddress { get; set; }

        /// <summary>
        /// Gets a value indicating whether a user defined name has been set for the device.
        /// </summary>
        protected bool UserDefinedNameSet => DeviceCardVm.UserDefinedName != null;

        /// <summary>
        /// Gets a value indicating whether the device is assigned to a profile or not.
        /// </summary>
        protected bool ProfileSet => DeviceCardVm.ProfileName != null;

        /// <summary>
        /// Gets a value indicating whether the device has been seen with a host name.
        /// </summary>
        protected bool HostNameSeen => DeviceCardVm.LastSeenHostName != null;

        /// <summary>
        /// Prepares the component.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task OnInitAsync()
        {
            DeviceCardVm = await DeviceService
                .Get()
                .Include(device => device.Profile)
                .Where(device => device.MacAddress == MacAddress)
                .Select(device => new DeviceCardVm
                {
                    MacAddress = device.MacAddress,
                    UserDefinedName = device.UserDefinedName,
                    ProfileName = device.Profile.Name,
                    LastSeenAt = device.LastSeenAt,
                    LastSeenHostName = device.LastSeenHostName,
                    LastSeenIp = device.LastSeenIp,
                })
                .SingleAsync();
        }
    }
}
