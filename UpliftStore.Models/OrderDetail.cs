using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpliftStore.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}
