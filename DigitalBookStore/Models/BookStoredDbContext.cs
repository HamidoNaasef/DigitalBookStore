using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DigitalBookStore.Models
{
    public class BookStoredDbContext: DbContext
    {
        public BookStoredDbContext(DbContextOptions<BookStoredDbContext> options) :base(options)
        {
            
        }
        public DbSet<Author> Authors{ get; set; }

        public DbSet<Book> books{ get; set; }
    }
}
