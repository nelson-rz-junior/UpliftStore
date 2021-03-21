using System;
using System.ComponentModel.DataAnnotations;

namespace UpliftStore.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(1000)]
        public string Comments { get; set; }

        public DateTime OrderCreate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int QuantityServices { get; set; }
    }
}
