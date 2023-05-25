using System.ComponentModel.DataAnnotations;

namespace Bilet_1.ViewMoldes
{
    public class CreatePostVM
    {
     
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
