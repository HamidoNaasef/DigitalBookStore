using DigitalBookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace DigitalBookStore.ViewModels
{
    public class BookAuthorViewModel{

        public int BookId { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        [MinLength(5)] 
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public List<Author> authors { get; set; }

        public IFormFile File{ get; set; }

        public string ImgUrl { get; set; } = string.Empty;


    }
}
