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
using System.Security.Claims;
using System.Threading.Tasks;
using Type = Library.Models.Type;

namespace Library.Controllers
{
    public class GenreController : Controller
    {
        // GET: GenreController
        IGenreService GenreService;

        public GenreController(IGenreService genreService)
        {
            GenreService = genreService;
        }
        
        public ActionResult Index()
        {
            
            IEnumerable<GenreDTO> genreDTOs = GenreService.GetGenres();
            List<Genre> genres = new List<Genre>();
            foreach (GenreDTO genreDTO in genreDTOs)
            {
                Genre genre = Mapper.Convert<GenreDTO, Genre>(genreDTO);
                genres.Add(genre);
            }

            return View(genres);
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                GenreDTO genreDTO = Mapper.Convert<Genre, GenreDTO>(genre);
                GenreService.CreateGenre(genreDTO);
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                GenreDTO genreDTO = GenreService.GetGenre(id);
                if (genreDTO != null)
                {
                    Genre genre = Mapper.Convert<GenreDTO, Genre>(genreDTO);
                    return View(genre);
                }
            }
            return NotFound();
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genre genre)
        {
            if (ModelState.IsValid)
            {
                GenreService.UpdateGenre(Mapper.Convert<Genre, GenreDTO>(genre));
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: GenreController/Delete/5
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            GenreDTO genreDTO = GenreService.GetGenre(id);
            if (genreDTO == null)
            {
                return NotFound();
            }
            Genre genre = Mapper.Convert<GenreDTO, Genre>(genreDTO);
            return View(genre);
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            GenreService.DeleteGenre(id);
            return RedirectToAction("Index");
        }
    }
}
