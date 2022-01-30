using System.ComponentModel.DataAnnotations;

namespace UpliftStore.Models
{
    public class WebImage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Picture { get; set; }
    }
}
