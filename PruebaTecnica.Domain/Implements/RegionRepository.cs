using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Data.Context;
using PruebaTecnica.Data.Entities;
using PruebaTecnica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PruebaTecnica.Domain.Implements
{
    public class RegionRepository:IRegionRepository
    {
        private readonly ApplicationDbContext context;
        
        public RegionRepository(IServiceProvider service)
        {
            this.context = service.GetRequiredService<ApplicationDbContext>();
        
        }

        public Region Delete(int id)
        {
            try
            {
                var r = context.Region.Find(id);
                if (r != null)
                {
                    context.Region.Remove(r);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Region no encotrado");
                }

                return r;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Save(Region region)
        {
            try
            {
                if (region.Id == null)
                {
                    context.Region.Add(region);
                }
                else
                {
                    var r = context.Region.Find(region.Id);
                    if (r != null)
                    {
                        r.Name = region.Name;
                    }
                }
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Region Get(int id)
        {
            try
            {
                //TODO: Cambiar
                var r = context.Region.FirstOrDefault(r => r.Id == id);

                r.RegionMunicipio = context.RegionMunicipio.Include(m => m.Municipio).Where(rm => rm.RegionId == r.Id && rm.Municipio.StatusId == 1).ToList();
                //r.RegionMunicipio.Where(m=m = r.Municipios?.Where(s => s.StatusId == 1).ToList();
                return r != null ? r : throw new Exception("Region no encotrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Region> List()
        {
            try
            {
                return context.Region.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
