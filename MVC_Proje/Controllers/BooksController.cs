using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Proje.Models;

namespace MVC_Proje.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString, int? categoryId, string sortOrder)
        {
            var books = _context.Books.Include(b => b.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                books = books.Where(b => b.CategoryId == categoryId);
            }
            books = sortOrder switch
            {
                "name_desc" => books.OrderByDescending(b => b.Title),
                "price_asc" => books.OrderBy(b => b.Price),
                "price_desc" => books.OrderByDescending(b => b.Price),
                "stock_asc" => books.OrderBy(b => b.Stock),
                "stock_desc" => books.OrderByDescending(b => b.Stock),
                _ => books.OrderBy(b => b.Title),
            };
            
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Categories = _context.Categories.ToList();

            return View(books.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Book newBook)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(newBook);
                _context.SaveChanges();
                TempData["Message"] = "Kitap başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(newBook);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var book = _context.Books.Find(id);
            ViewBag.Categories = _context.Categories.ToList();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Book updatedBook)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Update(updatedBook);
                _context.SaveChanges();
                TempData["Message"] = "Kitap başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(updatedBook);
        }

        public IActionResult Remove(int id)
        {
            var book = _context.Books.Find(id);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();

                TempData["Message"] = "Kitap başarıyla silindi.";
            }

            return RedirectToAction("Index");
        }
    }
}