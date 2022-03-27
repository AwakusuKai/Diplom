using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        // GET: BookController
        IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        
        public ActionResult Index()
        {
            IEnumerable<BookDTO> bookDTOs = bookService.GetBooks();
            List<Book> books = new List<Book>();
            foreach (BookDTO bookDTO in bookDTOs)
            {
                Book book = Mapper.Convert<BookDTO, Book>(bookDTO);
                book.Type = Mapper.Convert<TypeDTO, Models.Type>(bookDTO.Type);
                book.Autor = Mapper.Convert<AutorDTO, Autor>(bookDTO.Autor);
                book.Genre = Mapper.Convert<GenreDTO, Genre>(bookDTO.Genre);
                book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(bookDTO.PublishingHouse);
                books.Add(book);
            }

            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
