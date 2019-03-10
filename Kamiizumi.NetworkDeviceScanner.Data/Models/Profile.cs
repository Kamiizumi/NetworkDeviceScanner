using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kamiizumi.NetworkDeviceScanner.Data.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
