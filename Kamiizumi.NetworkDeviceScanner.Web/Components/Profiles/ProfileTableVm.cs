namespace Kamiizumi.NetworkDeviceScanner.Web.Components.Profiles
{
    /// <summary>
    /// View model for a profile in the <see cref="ProfileTableComponent"/>.
    /// </summary>
    public class ProfileTableVm
    {
        /// <summary>
        /// Gets or sets the database ID of the profile.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the profile.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of devices associated with the profile.
        /// </summary>
        public int DeviceCount { get; set; }
    }
}
