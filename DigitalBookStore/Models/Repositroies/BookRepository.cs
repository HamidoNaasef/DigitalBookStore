using Microsoft.EntityFrameworkCore;

namespace DigitalBookStore.Models.Repositroies
{
    public class BookRepository : IRepository<Book>
    {
        List<Book> books;

        // Temp Alt for Database
        public BookRepository(){
            books = new List<Book>() {
                new Book{Id = 1,
                    Title = "iSA Road To Be Hired Learning ASP.Net" ,
                    Description = "doing our best to get hired before Ramadan",
                    ImageUrl = "csphoto.png",
                    Author = new Author{ } },
                new Book{Id = 2,
                    Title = "iSA Road To Be Hired Learning MEAN Development" ,
                    Description = "doing our best to get hired before Ramadan",
                    ImageUrl = "mean.png",
                    Author = new Author{ }},
                new Book{Id = 3,
                    Title = "iSA Road To Be Hired Learning MERN Development" ,
                    Description = "doing our best to get hired before Ramadan",
                    ImageUrl = "mern.png",
                    Author = new Author{ }}
            };
        }
        public void Add(Book entity){
            entity.Id = books.Max(b=> b.Id) + 1;
            books.Add(entity);
        }
        
        public void Delete(int id){
            var dbook = Find(id);

            books.Remove(dbook);
        }
        
        public Book Find(int id){
            var fbook =  books.SingleOrDefault(b => b.Id == id);
            
            return fbook;
        }
        
        public IList<Book> List(){
            return books;
        }

        public IList<Book> Search(string term)
        {
            return books.Where(b => b.Author.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Book newBook){
            var ubook = Find(id);

            ubook.Title = newBook.Title;
            ubook.Description = newBook.Description;
            ubook.Author = newBook.Author;
            ubook.ImageUrl = newBook.ImageUrl;
        }
    }
}
