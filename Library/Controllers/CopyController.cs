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
    [Authorize(Roles = "admin")]
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
            IEnumerable<CopyDTO> copyDTOs = BookService.GetCopies(book); 


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

        // GET: CopyController/Details/5
        public ActionResult Details(int id)
        {
            CopyDTO copyDTO = BookService.GetCopy(id);
            if (copyDTO != null)
            {
                Copy copy = Mapper.Convert<CopyDTO, Copy>(copyDTO);
                copy.Book = Mapper.Convert<BookDTO, Book>(copyDTO.Book);
                copy.Book.Type = Mapper.Convert<TypeDTO, Models.Type>(copyDTO.Book.Type);
                copy.Book.Autor = Mapper.Convert<AutorDTO, Autor>(copyDTO.Book.Autor);
                copy.Book.Genre = Mapper.Convert<GenreDTO, Genre>(copyDTO.Book.Genre);
                copy.Book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(copyDTO.Book.PublishingHouse);
                return View(copy);
            }
            return NotFound();
        }

        // GET: CopyController/Create
        public ActionResult Create()
        {
            ViewData["BookId"] = new SelectList(Mapper.ConvertEnumerable<BookDTO, Book>(BookService.GetBooks(null,null,null,null)), "Id", "Name");
            return View();
        }

        // POST: CopyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Copy copy)
        {
            if (ModelState.IsValid)
            {
                CopyDTO copyDTO = Mapper.Convert<Copy, CopyDTO>(copy);
                copyDTO.Status = 0;
                BookService.CreateCopy(copyDTO);
                return RedirectToAction("Index");
            }
            return View(copy);
        }

        // GET: CopyController/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["BookId"] = new SelectList(Mapper.ConvertEnumerable<BookDTO, Book>(BookService.GetBooks(null, null, null, null)), "Id", "Name");
            
            if (id != null)
            {
                CopyDTO copyDTO = BookService.GetCopy(id);
                if (copyDTO != null)
                {
                    Copy copy = Mapper.Convert<CopyDTO, Copy>(copyDTO);
                    copy.Book = Mapper.Convert<BookDTO, Book>(copyDTO.Book);
                    copy.Book.Type = Mapper.Convert<TypeDTO, Models.Type>(copyDTO.Book.Type);
                    copy.Book.Autor = Mapper.Convert<AutorDTO, Autor>(copyDTO.Book.Autor);
                    copy.Book.Genre = Mapper.Convert<GenreDTO, Genre>(copyDTO.Book.Genre);
                    copy.Book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(copyDTO.Book.PublishingHouse);
                    return View(copy);
                }
            }
            return NotFound();
        }

        // POST: CopyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Copy copy)
        {
            if (ModelState.IsValid)
            {
                BookService.UpdateCopy(Mapper.Convert<Copy, CopyDTO>(copy));
                return RedirectToAction("Index");
            }
            return View(copy);
        }

        // GET: CopyController/Delete/5
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            CopyDTO copyDTO = BookService.GetCopy(id);
            if (copyDTO == null)
            {
                return NotFound();
            }
            Copy copy = Mapper.Convert<CopyDTO, Copy>(copyDTO);
            copy.Book = Mapper.Convert<BookDTO, Book>(copyDTO.Book);
            copy.Book.Type = Mapper.Convert<TypeDTO, Models.Type>(copyDTO.Book.Type);
            copy.Book.Autor = Mapper.Convert<AutorDTO, Autor>(copyDTO.Book.Autor);
            copy.Book.Genre = Mapper.Convert<GenreDTO, Genre>(copyDTO.Book.Genre);
            copy.Book.PublishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(copyDTO.Book.PublishingHouse);
            return View(copy);
        }

        // POST: CopyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            BookService.DeleteCopy(id);
            return RedirectToAction("Index");
        }
    }
}
