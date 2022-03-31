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
    public class TypeController : Controller
    {
        // GET: TypeController
        ITypeService TypeService;

        public TypeController(ITypeService typeService)
        {
            TypeService = typeService;
        }
        
        public ActionResult Index()
        {
            IEnumerable<TypeDTO> typeDTOs = TypeService.GetTypes();
            List<Type> types = new List<Type>();
            foreach (TypeDTO typeDTO in typeDTOs)
            {
                Type type = Mapper.Convert<TypeDTO, Type>(typeDTO);
                types.Add(type);
            }

            return View(types);
        }

        // GET: TypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Type type)
        {
            if (ModelState.IsValid)
            {
                TypeDTO typeDTO = Mapper.Convert<Type, TypeDTO>(type);
                TypeService.CreateType(typeDTO);
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // GET: TypeController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                TypeDTO typeDTO = TypeService.GetType(id);
                if (typeDTO != null)
                {
                    Type type = Mapper.Convert<TypeDTO, Type>(typeDTO);
                    return View(type);
                }
            }
            return NotFound();
        }

        // POST: TypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Type type)
        {
            if (ModelState.IsValid)
            {
                TypeService.UpdateType(Mapper.Convert<Type, TypeDTO>(type));
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // GET: TypeController/Delete/5
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            TypeDTO typeDTO = TypeService.GetType(id);
            if (typeDTO == null)
            {
                return NotFound();
            }
            Type type = Mapper.Convert<TypeDTO, Type>(typeDTO);
            return View(type);
        }

        // POST: TypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            TypeService.DeleteType(id);
            return RedirectToAction("Index");
        }
    }
}
