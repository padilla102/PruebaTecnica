using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Data.Context;
using PruebaTecnica.Data.Entities;
using PruebaTecnica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Domain.Implements
{
    public class RegionMunicipioRepository : IRegionMunicipioRepository
    {
        private readonly ApplicationDbContext context;

        public RegionMunicipioRepository(IServiceProvider service)
        {
            this.context = service.GetRequiredService<ApplicationDbContext>();

        }
        public RegionMunicipio Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(RegionMunicipio regionMunicipio)
        {
            try
            {
                context.RegionMunicipio.Add(regionMunicipio);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
