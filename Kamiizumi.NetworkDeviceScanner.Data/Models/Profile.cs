namespace Kamiizumi.NetworkDeviceScanner.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a profile a device can belong to.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets or sets the unique identifier for the profile.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the profile.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the devices that are assigned to the profile.
        /// </summary>
        public virtual ICollection<Device> Devices { get; set; }
    }
}
