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

        [Required]
        public DateTimeOffset? LastSeen { get; set; }
    }
}
