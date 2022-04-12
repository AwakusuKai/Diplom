using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class BookController : Controller
    {
        // GET: BookController
        IBookService BookService;
        IAutorService AutorService;
        ITypeService TypeService;
        IGenreService GenreService;
        IPHService PHService;

        public BookController(IBookService bookService, IAutorService autorService, ITypeService typeService, IGenreService genreService, IPHService phService)
        {
            BookService = bookService;
            AutorService = autorService;
            TypeService = typeService;
            GenreService = genreService;
            PHService = phService;

        }
        
        public ActionResult Index(int? autor, int? genre, int? ph, string name)
        {
            IEnumerable<BookDTO> bookDTOs = BookService.GetBooks(autor,genre, ph,  name); //используя параметры, отсортировать выдачу.


            List<Book> books = new List<Book>();
            foreach (BookDTO bookDTO in bookDTOs)
            {
                Book book = Mapper.Convert<BookDTO, Book>(bookDTO);
                book.Type = Mapper.Convert<TypeDTO, Models.Type>(bookDTO.Type);
                book.Autor = Mapper.Convert<AutorDTO, Autor>(bookDTO.Autor);
                book.Genre = Mapper.Convert<GenreDTO, Genre>(bookDTO.Genre);
                book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(bookDTO.PublishingHouse);
                book.BookStatus = BookService.IsFreeCopiesExist(bookDTO.Id);
                books.Add(book);
            }
            List<Autor> autors = new List<Autor>(Mapper.ConvertEnumerable<AutorDTO, Autor>(AutorService.GetAutors()));
            autors.Insert(0, new Autor { Name = "Все", Id = 0 });
            List<Genre> genres = new List<Genre>(Mapper.ConvertEnumerable<GenreDTO, Genre>(GenreService.GetGenres()));
            genres.Insert(0, new Genre { Name = "Все", Id = 0 });
            List<Type> types = new List<Type>(Mapper.ConvertEnumerable<TypeDTO, Type>(TypeService.GetTypes()));
            types.Insert(0, new Type { Name = "Все", Id = 0 });
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>(Mapper.ConvertEnumerable<PublishingHouseDTO, PublishingHouse>(PHService.GetPHs()));
            publishingHouses.Insert(0, new PublishingHouse { Name = "Все", Id = 0 });



            BookListViewModel bookListViewModel = new BookListViewModel
            {
                Books = books,
                Autors = new SelectList(autors, "Id", "Name"),
                Genres = new SelectList(genres, "Id", "Name"),
                PublishingHouses = new SelectList(publishingHouses, "Id", "Name"),
                Name = name
            };

            return View(bookListViewModel);
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
                book.BookStatus = BookService.IsFreeCopiesExist(bookDTO.Id);
                return View(book);
            }
            return NotFound();
        }

        // GET: BookController/Create
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
