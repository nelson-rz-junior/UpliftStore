using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpliftStore.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Display Name")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [StringLength(150)]
        public string ImageFile { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [Display(Name = "Frequency")]
        public int FrequencyId { get; set; }

        [ForeignKey(nameof(FrequencyId))]
        public virtual Frequency Frequency { get; set; }
    }
}
