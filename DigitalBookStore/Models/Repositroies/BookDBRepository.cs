using Microsoft.EntityFrameworkCore;

namespace DigitalBookStore.Models.Repositroies
{
    public class BookDBRepository : IRepository<Book>
    {
        List<Book> books;

        // Temp Alt for Database
        BookStoredDbContext dbContext;

        public BookDBRepository(BookStoredDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public void Add(Book entity)
        {
            dbContext.books.Add(entity);
            dbContext.SaveChanges();
        }
        public void Update(int id, Book newBook)
        {
            dbContext.books.Update(newBook);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbook = Find(id);

            dbContext.books.Remove(dbook);
            dbContext.SaveChanges();
        }

        public Book Find(int id)
        {
            var fbook = dbContext.books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);

            return fbook;
        }

        public IList<Book> List()
        {
            return dbContext.books.Include(a=> a.Author).ToList();
        }
        public IList<Book> Search(string term)
        {
            var result = dbContext.books.Include(a=> a.Author).
                Where(b => b.Title.Contains(term) 
                   || b.Description.Contains(term)
                   || b.Author.FullName.Contains(term)).ToList();
            return result;
        }

    }
}
