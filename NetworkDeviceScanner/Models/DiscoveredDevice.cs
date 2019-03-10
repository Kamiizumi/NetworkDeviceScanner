using System;
using System.ComponentModel.DataAnnotations;

namespace NetworkDeviceScanner.Models
{
    public class DiscoveredDevice
    {
        [Key]
        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string MacAddress { get; set; }

        [MaxLength(255)]
        public string CustomName { get; set; }

        [Required]
        public DateTimeOffset? LastSeen { get; set; }
    }
}
