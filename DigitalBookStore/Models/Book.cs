using System.ComponentModel.DataAnnotations;

namespace DigitalBookStore.Models
{
    public class Book{

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        [MinLength(5)]
        public string Description { get; set; } = String.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    
        public Author Author { get; set; }
    }
}
