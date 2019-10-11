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
        private readonly Context context;
        public MunicipioRepository(IServiceProvider service)
        {
            this.context = service.GetRequiredService<Context>();
        }
        public bool Create(Municipio municipio)
        {
            try
            {
                context.Municipio.Add(municipio);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var m = context.Municipio.Find(id);
                context.Municipio.Remove(m);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Edit(Municipio municipio)
        {
            try
            {

                var m = context.Municipio.Find(municipio.Id);
                m.Name = municipio.Name;
                m.status = municipio.status;
                var result = context.SaveChanges();

                return true;
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
                return context.Municipio.Find(id);
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
