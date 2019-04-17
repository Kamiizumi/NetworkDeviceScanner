namespace Kamiizumi.NetworkDeviceScanner.Web.Components.Devices
{
    using System;

    /// <summary>
    /// View model for <see cref="DeviceCardComponent"/>.
    /// </summary>
    public class DeviceCardVm
    {
        /// <summary>
        /// Gets or sets the user defined name for the device.
        /// </summary>
        public string UserDefinedName { get; set; }

        /// <summary>
        /// Gets or sets the name of the profile the device is assigned to.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the time the device was last seen.
        /// </summary>
        public DateTimeOffset? LastSeenAt { get; set; }

        /// <summary>
        /// Gets or sets the IP address the device was last seen with.
        /// </summary>
        public string LastSeenIp { get; set; }

        /// <summary>
        /// Gets or sets the host name the device was last seen with.
        /// </summary>
        public string LastSeenHostName { get; set; }
    }
}
