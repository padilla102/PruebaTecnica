using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaTecnica.Data.Entities;
using PruebaTecnica.Domain.Abstract;
using PruebaTecnica.WebUI.Models;

namespace PruebaTecnica.WebUI.Controllers
{
    public class MunicipioController : Controller
    {
        private readonly IMunicipioRepository repository;
        public int PageSize = 5;

        public MunicipioController(IMunicipioRepository repository)
        {
            this.repository = repository;
        }

        // GET: Municipio
        public ActionResult List(int municipioPage = 1)
        {
            try
            {
                var list = repository.List();
                return View(new MunicipioListViewModel
                {
                    Municipios = list
                                .OrderBy(p => p.Id)
                                .Skip((municipioPage - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = municipioPage,
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
            return View("Edit",new MunicipioViewModel());
        }

        // GET: Municipio/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var m = repository.Get(id);
                MunicipioViewModel vm = new MunicipioViewModel
                {
                    Municipio = m
                };
                return View(vm);
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
        public ActionResult Edit(Municipio municipio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    municipio.RegionId = 1;
                    repository.Save(municipio);
                    TempData["message"] = $"{municipio.Name} se ha registrado";
                    return RedirectToAction("List");
                }
                else
                {
                    // there is something wrong with the data values
                    return View(municipio);
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