using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly IMunicipioRepository repoMunicipio;
        private readonly IRegionMunicipioRepository repoRegionMunicipio;
        public int PageSize = 5;

        public RegionController(IRegionRepository repository, IMunicipioRepository repoMunicipio, IRegionMunicipioRepository repoRegionMunicipio)
        {
            this.repository = repository;
            this.repoMunicipio = repoMunicipio;
            this.repoRegionMunicipio = repoRegionMunicipio;
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
            //TODO: Cambiar
            //return View("Edit", new Region { Municipios = new List<Municipio>() });
            RegionViewModel model = new RegionViewModel
            {
                Region = new Region { RegionMunicipio = null },
                ListMunicipio = repoMunicipio.List()
            };
            return View("Edit",model);
        }

        // GET: Municipio/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var listMunicipio = repoMunicipio.List();
                var m = repository.Get(id);

                IEnumerable<Municipio> list = (from lm in listMunicipio
                                               join rm in m.RegionMunicipio
                                                  on lm.Id equals rm.MunicipioId into a
                                               from x in a.DefaultIfEmpty()
                                               select new Municipio
                                               {
                                                   Id = lm.Id,
                                                   Name = lm.Name,
                                                   StatusId = x != null ? 2 : lm.StatusId
                                               }).ToList().Where(x => x.StatusId == 1);
                
                RegionViewModel model = new RegionViewModel
                {
                    Region = m,
                    ListMunicipio = list
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View("List");
            }

        }

        [HttpPost]
        public ActionResult AddMunicipio(RegionViewModel model)
        {
            try
            {
                repoRegionMunicipio.Save(new RegionMunicipio
                {
                    MunicipioId = (int)model.MunicipioId,
                    RegionId = (int)model.Region.Id
                });
                TempData["message"] = $"Procesado Exitosamente!!";
                return RedirectToAction("Edit", new { @id = model.Region.Id });
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return RedirectToAction("Edit", new { @id = model.Region.Id });
            }
        }

        // POST: Municipio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Save(model.Region);
                    TempData["message"] = $"Procesado Exitosamente!!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View(model);
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