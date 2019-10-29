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
        private readonly IStatusRepository repoStatus;
        public int PageSize = 2;

        public MunicipioController(IMunicipioRepository repository, IStatusRepository repoStatus)
        {
            this.repository = repository;
            this.repoStatus = repoStatus;
        }

        // GET: Municipio
        [HttpGet]
        public ActionResult List(int page = 1)
        {
            try
            {
                var list = repository.List();

                var model = new MunicipioListViewModel
                {
                    Municipios = list
                                .OrderBy(p => p.Id)
                                .Skip((page - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = list.Count()
                    }
                };
                return View(model);

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
            try
            {
                var model = new MunicipioViewModel
                {
                    Municipio = new Municipio { RegionMunicipio = null },
                    ListStatus = repoStatus.List()
                };

                return View("Edit", model);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View("List");
            }
            
        }

        // GET: Municipio/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var m = repository.Get(id);
                MunicipioViewModel vm = new MunicipioViewModel
                {
                    Municipio = m,
                    ListStatus = repoStatus.List()
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View("List");
            }
            
        }

        // POST: Municipio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MunicipioViewModel model)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    repository.Save(model.Municipio);
                    TempData["message"] = $"{model.Municipio.Name} se ha registrado";
                    return RedirectToAction("List");
                }
                else
                {
                    // there is something wrong with the data values
                    
                        MunicipioViewModel vm = new MunicipioViewModel
                        {
                            Municipio = model.Municipio,
                            ListStatus = repoStatus.List()
                        };
                    return View(vm);

                }
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View(model.Municipio);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}