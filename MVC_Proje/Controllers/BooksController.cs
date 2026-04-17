using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Add() => View();

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
            return View(newBook);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var book = _context.Books.Find(id);
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