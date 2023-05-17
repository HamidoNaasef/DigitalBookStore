using DigitalBookStore.Models;
using DigitalBookStore.Models.Repositroies;
using DigitalBookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DigitalBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IRepository<Book> book_repositry;

        private readonly IRepository<Author> author_Repo ;
    
        private readonly IHostingEnvironment hosting ;

        public BookController(IRepository<Book> book_repo, IRepository<Author> author_repo, IHostingEnvironment hosting) {
            this.book_repositry = book_repo;
            this.author_Repo = author_repo;
            this.hosting = hosting;
        }

        // GET: BookController
        public ActionResult Index(){
            var book_list = book_repositry.List();
            return View(book_list);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id){
            var book = book_repositry.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create(){

            var model = new BookAuthorViewModel{
                authors = AuthorsList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            try{
                if (model.AuthorId == -1) {
                    ViewBag.message = "Please Choose an Author";
                    model.authors = AuthorsList();
                    return View(model);
                }
                //uploading the new file here
                string file_name = UploadFile(model.File) ? model.File.FileName : string.Empty;

                var bookAuthor = author_Repo.Find(model.AuthorId);

                Book new_book = new Book {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = bookAuthor,
                    ImageUrl = file_name
                };
                book_repositry.Add(new_book);
                return RedirectToAction(nameof(Index));
            }
            catch{
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = book_repositry.Find(id);
            
            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                Description = book.Description,
                Title = book.Title,
                AuthorId = book.Author.Id,
                authors = AuthorsList(),
                ImgUrl = book.ImageUrl
            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            try
            {
                if (viewModel.AuthorId == -1)
                {
                    ViewBag.message = "Please Choose an Author";
                    viewModel.authors = AuthorsList();
                    return View(viewModel);
                }

                string file_name= UploadFile(viewModel.File, viewModel.ImgUrl) ? viewModel.File.FileName : viewModel.ImgUrl;

                //right
                var bookauthor = author_Repo.Find(viewModel.AuthorId);
                Book new_book = new Book
                {
                    Id = viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = bookauthor,
                    ImageUrl = file_name
                };
                book_repositry.Update(new_book.Id, new_book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = book_repositry.Find(id);

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                book_repositry.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string term) 
        {
            var result = book_repositry.Search(term);

            return View("Index", result);
        }
        List<Author> AuthorsList (){
            List<Author> authorList = new List<Author>();
            authorList = author_Repo.List().ToList();
            authorList.Insert(0, new Author { Id = -1, FullName = " ... Please Choose Author ..."});
            return authorList;
        }

        bool UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string full_path = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(full_path, FileMode.Create));
                return true;
            }
            return false;
        }
        bool UploadFile(IFormFile file, string imgURL)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string new_path = Path.Combine(uploads, file.FileName);

                string old_path = Path.Combine(uploads, imgURL);

                if (new_path != old_path)
                {
                    System.IO.File.Delete(old_path);
                    //save new file
                    if (!System.IO.File.Exists(new_path))
                    {
                        file.CopyTo(new FileStream(new_path, FileMode.Create));
                    }

                }
                return true;
            }
            return false;
        }

    }
}
