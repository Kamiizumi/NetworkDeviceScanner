namespace Kamiizumi.NetworkDeviceScanner.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a network device.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the MAC address of the device.
        /// </summary>
        [Key]
        [Required]
        [MaxLength(17)]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets a custom friendly name for the device.
        /// </summary>
        [MaxLength(255)]
        public string UserDefinedName { get; set; }

        /// <summary>
        /// Gets or sets the IP address the device was last seen at.
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string LastSeenIp { get; set; }

        /// <summary>
        /// Gets or sets the host name the device was last seen with.
        /// </summary>
        [MaxLength(63)]
        public string LastSeenHostName { get; set; }

        /// <summary>
        /// Gets or sets the time the device was last seen at.
        /// </summary>
        [Required]
        public DateTimeOffset? LastSeenAt { get; set; }

        /// <summary>
        /// Gets or sets the profile the device has been associated with.
        /// </summary>
        public virtual Profile Profile { get; set; }
    }
}
