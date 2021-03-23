using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UpliftStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }
    }
}
