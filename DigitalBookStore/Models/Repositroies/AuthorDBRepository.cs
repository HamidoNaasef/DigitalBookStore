using Microsoft.EntityFrameworkCore;

namespace DigitalBookStore.Models.Repositroies
{
    public class AuthorDBRepository : IRepository<Author>
    {
        BookStoredDbContext dbContext;

        public AuthorDBRepository(BookStoredDbContext    _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Add(Author author)
        {
            dbContext.Authors.Add(author);
            dbContext.SaveChanges();
        }
        public void Update(int id, Author newAuthor)
        {
            dbContext.Authors.Update(newAuthor);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);

            dbContext.Authors.Remove(author);
            dbContext.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = dbContext.Authors.SingleOrDefault(b => b.Id == id);

            return author;
        }

        public IList<Author> List()
        {
            return dbContext.Authors.ToList();
        }

        public IList<Author> Search(string term)
        {
            return dbContext.Authors.Where(a=> a.FullName.Contains(term)).ToList();
        }
    }
}
