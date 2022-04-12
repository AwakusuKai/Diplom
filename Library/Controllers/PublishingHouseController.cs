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

namespace Library.Controllers
{
    [Authorize(Roles = "admin")]
    public class PublishingHouseController : Controller
    {
        // GET: PHController
        IPHService PHService;

        public PublishingHouseController(IPHService phService)
        {
            PHService = phService;
        }
        
        public ActionResult Index()
        {
            IEnumerable<PublishingHouseDTO> publishingHouseDTOs = PHService.GetPHs();
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>();
            foreach (PublishingHouseDTO publishingHouseDTO in publishingHouseDTOs)
            {
                PublishingHouse publishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(publishingHouseDTO);
                publishingHouses.Add(publishingHouse);
            }

            return View(publishingHouses);
        }

        // GET: PHController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PHController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                PublishingHouseDTO publishingHouseDTO = Mapper.Convert<PublishingHouse, PublishingHouseDTO>(publishingHouse);
                PHService.CreatePH(publishingHouseDTO);
                return RedirectToAction("Index");
            }
            return View(publishingHouse);
        }

        // GET: PHController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                PublishingHouseDTO publishingHouseDTO = PHService.GetPH(id);
                if (publishingHouseDTO != null)
                {
                    PublishingHouse publishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(publishingHouseDTO);
                    return View(publishingHouse);
                }
            }
            return NotFound();
        }

        // POST: PHController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                PHService.UpdatePH(Mapper.Convert<PublishingHouse, PublishingHouseDTO>(publishingHouse));
                return RedirectToAction("Index");
            }
            return View(publishingHouse);
        }

        // GET: PHController/Delete/5
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            PublishingHouseDTO publishingHouseDTO = PHService.GetPH(id);
            if (publishingHouseDTO == null)
            {
                return NotFound();
            }
            PublishingHouse publishingHouse = Mapper.Convert<PublishingHouseDTO, PublishingHouse>(publishingHouseDTO);
            return View(publishingHouse);
        }

        // POST: PHController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            PHService.DeletePH(id);
            return RedirectToAction("Index");
        }
    }
}
