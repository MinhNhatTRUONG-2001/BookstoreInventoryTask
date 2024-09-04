using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookCrudOperationASP_Net.Data;
using Microsoft.AspNetCore.Mvc;
using BookCrudOperationASP_Net.Models;

namespace BookCrudOperationASP_Net.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_booksRepository.GetAllBooks());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = _booksRepository.AddBook(book);
                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Book creation failed.");
                }
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var book = _booksRepository.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Book objBook)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = _booksRepository.UpdateBook(objBook);
                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Book update failed.");
                }
            }
            return View(objBook);
        }

        public IActionResult Delete(int id)
        {
            bool isSuccess = _booksRepository.DeleteBook(id);
            if (isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
