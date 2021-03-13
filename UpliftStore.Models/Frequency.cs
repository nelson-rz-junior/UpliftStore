using System.ComponentModel.DataAnnotations;

namespace UpliftStore.Models
{
    public class Frequency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Frequency Name")]
        [StringLength(100)]
        public string Name { get; set; }

        public int Count { get; set; }
    }
}
