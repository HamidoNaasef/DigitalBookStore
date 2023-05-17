using DigitalBookStore.Models;
using DigitalBookStore.Models.Repositroies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IRepository<Author> author_repository;
        public AuthorController(IRepository<Author> authorRepo) { 
            
            this.author_repository = authorRepo;
        }


        // GET: AuthorController
        public ActionResult Index(){
            var authors = author_repository.List();

            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id){
            var authorDetails = author_repository.Find(id);

            return View(authorDetails);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try {
                author_repository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch {
                
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var edited_author = author_repository.Find(id);

            return View(edited_author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try{
                author_repository.Update(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var deleted_author = author_repository.Find(id);

            return View(deleted_author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try{
                author_repository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
