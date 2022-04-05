using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = Library.Models.Type;

namespace Library.Controllers
{
    public class CopyController : Controller
    {
        // GET: CopyController
        IBookService BookService;
        IAutorService AutorService;
        ITypeService TypeService;
        IGenreService GenreService;
        IPHService PHService;

        public CopyController(IBookService bookService, IAutorService autorService, ITypeService typeService, IGenreService genreService, IPHService phService)
        {
            BookService = bookService;
            AutorService = autorService;
            TypeService = typeService;
            GenreService = genreService;
            PHService = phService;

        }
        
        public ActionResult Index(int? book)
        {
            IEnumerable<CopyDTO> copyDTOs = BookService.GetCopies(book); //используя параметры, отсортировать выдачу.


            List<Copy> copies = new List<Copy>();
            foreach (CopyDTO copyDTO in copyDTOs)
            {
                Copy copy = Mapper.Convert<CopyDTO, Copy>(copyDTO);
                copy.Book = Mapper.Convert<BookDTO, Book>(copyDTO.Book);
                copy.Book.Type = Mapper.Convert<TypeDTO, Models.Type>(copyDTO.Book.Type);
                copy.Book.Autor = Mapper.Convert<AutorDTO, Autor>(copyDTO.Book.Autor);
                copy.Book.Genre = Mapper.Convert<GenreDTO, Genre>(copyDTO.Book.Genre);
                copy.Book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(copyDTO.Book.PublishingHouse);
                copies.Add(copy);
            }
            List<Book> books = new List<Book>(Mapper.ConvertEnumerable<BookDTO, Book>(BookService.GetBooks(null,null,null,null)));
            books.Insert(0, new Book { Name = "Все", Id = 0 });

            CopyListViewModel copyListViewModel = new CopyListViewModel
            {
                Copies = copies,
                Books = new SelectList(books, "Id", "Name")
            };

            return View(copyListViewModel);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            BookDTO bookDTO = BookService.GetBook(id);
            if (bookDTO != null)
            {
                Book book = Mapper.Convert<BookDTO, Book>(bookDTO);
                book.Autor = Mapper.Convert<AutorDTO, Autor>(bookDTO.Autor);
                book.Genre = Mapper.Convert<GenreDTO, Genre>(bookDTO.Genre);
                book.Type = Mapper.Convert<TypeDTO, Type>(bookDTO.Type);
                book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(bookDTO.PublishingHouse);
                return View(book);
            }
            return NotFound();
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(Mapper.ConvertEnumerable<AutorDTO, Autor>(AutorService.GetAutors()), "Id", "Name");
            ViewData["TypeId"] = new SelectList(Mapper.ConvertEnumerable<TypeDTO, Type>(TypeService.GetTypes()), "Id", "Name");
            ViewData["GenreId"] = new SelectList(Mapper.ConvertEnumerable<GenreDTO, Genre>(GenreService.GetGenres()), "Id", "Name");
            ViewData["PHId"] = new SelectList(Mapper.ConvertEnumerable<PublishingHouseDTO, PublishingHouse>(PHService.GetPHs()), "Id", "Name");
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                BookDTO bookDTO = Mapper.Convert<Book, BookDTO>(book);
                BookService.CreateBook(bookDTO);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["AutorId"] = new SelectList(Mapper.ConvertEnumerable<AutorDTO, Autor>(AutorService.GetAutors()), "Id", "Name");
            ViewData["TypeId"] = new SelectList(Mapper.ConvertEnumerable<TypeDTO, Type>(TypeService.GetTypes()), "Id", "Name");
            ViewData["GenreId"] = new SelectList(Mapper.ConvertEnumerable<GenreDTO, Genre>(GenreService.GetGenres()), "Id", "Name");
            ViewData["PHId"] = new SelectList(Mapper.ConvertEnumerable<PublishingHouseDTO, PublishingHouse>(PHService.GetPHs()), "Id", "Name");
            if (id != null)
            {
                BookDTO bookDTO = BookService.GetBook(id);
                if (bookDTO != null)
                {
                    Book book = Mapper.Convert<BookDTO, Book>(bookDTO);
                    book.Autor = Mapper.Convert<AutorDTO, Autor>(bookDTO.Autor);
                    book.Genre = Mapper.Convert<GenreDTO, Genre>(bookDTO.Genre);
                    book.Type = Mapper.Convert<TypeDTO, Type>(bookDTO.Type);
                    book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(bookDTO.PublishingHouse);
                    return View(book);
                }
            }
            return NotFound();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                BookService.UpdateBook(Mapper.Convert<Book, BookDTO>(book));
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: BookController/Delete/5
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            BookDTO bookDTO = BookService.GetBook(id);
            if (bookDTO == null)
            {
                return NotFound();
            }
            Book book = Mapper.Convert<BookDTO, Book>(bookDTO);
            book.Autor = Mapper.Convert<AutorDTO, Autor>(bookDTO.Autor);
            book.Genre = Mapper.Convert<GenreDTO, Genre>(bookDTO.Genre);
            book.Type = Mapper.Convert<TypeDTO, Type>(bookDTO.Type);
            book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(bookDTO.PublishingHouse);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            BookService.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
