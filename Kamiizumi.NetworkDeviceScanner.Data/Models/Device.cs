using System;
using System.ComponentModel.DataAnnotations;

namespace Kamiizumi.NetworkDeviceScanner.Data.Models
{
    public class Device
    {
        [Key]
        [Required]
        [MaxLength(17)]
        public string MacAddress { get; set; }

        [MaxLength(255)]
        public string UserDefinedName { get; set; }

        [Required]
        [MaxLength(15)]
        public string LastSeenIp { get; set; }

        [MaxLength(63)]
        public string LastSeenHostName { get; set; }

        [Required]
        public DateTimeOffset? LastSeenAt { get; set; }
    }
}
