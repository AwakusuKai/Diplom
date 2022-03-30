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
    public class AutorController : Controller
    {
        // GET: AutorController
        IAutorService AutorService;

        public AutorController(IAutorService autorService)
        {
            AutorService = autorService;
        }
        
        public ActionResult Index()
        {
            IEnumerable<AutorDTO> autorDTOs = AutorService.GetAutors();
            List<Autor> autors = new List<Autor>();
            foreach (AutorDTO autorDTO in autorDTOs)
            {
                Autor autor = Mapper.Convert<AutorDTO, Autor>(autorDTO);
                autors.Add(autor);
            }

            return View(autors);
        }

        // GET: AutorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                AutorDTO autorDTO = Mapper.Convert<Autor, AutorDTO>(autor);
                AutorService.CreateAutor(autorDTO);
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: AutorController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                AutorDTO autorDTO = AutorService.GetAutor(id);
                if (autorDTO != null)
                {
                    Autor autor = Mapper.Convert<AutorDTO, Autor>(autorDTO);
                    return View(autor);
                }
            }
            return NotFound();
        }

        // POST: AutorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Autor autor)
        {
            if (ModelState.IsValid)
            {
                AutorService.UpdateAutor(Mapper.Convert<Autor, AutorDTO>(autor));
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: AutorController/Delete/5
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            AutorDTO autorDTO = AutorService.GetAutor(id);
            if (autorDTO == null)
            {
                return NotFound();
            }
            Autor autor = Mapper.Convert<AutorDTO, Autor>(autorDTO);
            return View(autor);
        }

        // POST: AutorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            AutorService.DeleteAutor(id);
            return RedirectToAction("Index");
        }
    }
}
