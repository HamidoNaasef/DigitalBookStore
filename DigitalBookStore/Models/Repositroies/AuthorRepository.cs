using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace DigitalBookStore.Models.Repositroies
{
    public class AuthorRepository : IRepository<Author>
    {
        IList<Author> authors;

        public AuthorRepository(){
            authors = new List<Author>() { 
                new Author{ Id = 1, FullName = "Abd El-Hamid Nasef Abd El-Hamid"},
                new Author{ Id = 2, FullName = "Amr Nasef Abd El-Hamid"},
                new Author{ Id = 3, FullName = "Hassan Nasef Abd El-Hamid"},
            };
        }

        public void Add(Author entity)
        {
           entity.Id = authors.Max(a=> a.Id) + 1;
           authors.Add(entity);
        }

        public void Delete(int id)
        {
            var del_author = Find(id);

            authors.Remove(del_author);
        }

        public Author Find(int id)
        {
            var find_author = authors.SingleOrDefault(b => b.Id == id);

            return find_author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public IList<Author> Search(string term)
        {
            return authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            var update_author = Find(id);

            update_author.FullName = newAuthor.FullName;
        }
    }
}
