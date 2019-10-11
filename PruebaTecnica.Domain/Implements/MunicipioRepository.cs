using AutoMapper;
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
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public MunicipioRepository(IServiceProvider service, IMapper mapper)
        {
            this.context = service.GetRequiredService<ApplicationDbContext>();
            this.mapper = mapper;
        }

        public Municipio Delete(int id)
        {
            try
            {
                var m = context.Municipio.Find(id);
                if (m != null)
                {
                    context.Municipio.Remove(m);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Municipio no encotrado");
                }

                return m;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Save(Municipio municipio)
        {
            try
            {
                if (municipio.Id == null)
                {
                    context.Municipio.Add(municipio);
                }
                else
                {
                    var m = context.Municipio.Find(municipio.Id);
                    if (m != null)
                    {
                        m.Name = municipio.Name;
                        m.status = municipio.status;
                        m.RegionId = 1;
                        //m.Region = new Region { Id = 1, Name = "Andina", Municipios = null };
                    }
                }
                context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Municipio Get(int id)
        {
            try
            {
                var m = context.Municipio.Find(id);
                return m!= null ? m: throw new Exception("Municipio no encotrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Municipio> List()
        {
            try
            {
                return context.Municipio.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
