using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Data.Entities;
using PruebaTecnica.Domain.Abstract;
using PruebaTecnica.WebUI.Models;

namespace PruebaTecnica.WebUI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionRepository repository;
        public int PageSize = 5;

        public RegionController(IRegionRepository repository)
        {
            this.repository = repository;
        }

        // GET: Municipio
        public ActionResult List(int page = 1)
        {
            try
            {
                var list = repository.List();

                return View(new RegionListViewModel
                {
                    Regions = list
                                .OrderBy(p => p.Id)
                                .Skip((page - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = list.Count()
                    }
                });


            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View();
            }
        }

        // GET: Municipio/Create
        public ActionResult Create()
        {
            return View("Edit", new Region());
        }

        // GET: Municipio/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var m = repository.Get(id);
                return View(m);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View();
            }

        }

        // POST: Municipio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Region region)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Save(region);
                    TempData["message"] = $"{region.Name} se ha registrado";
                    return RedirectToAction("List");
                }
                else
                {
                    // there is something wrong with the data values
                    return View(region);
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View();
            }
        }

        // GET: Municipio/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                repository.Delete(id);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View();
            }
        }
    }
}