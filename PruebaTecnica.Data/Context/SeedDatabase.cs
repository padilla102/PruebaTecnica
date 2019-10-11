using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PruebaTecnica.Data.Context
{
    public static class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<Context>();

            context.Database.EnsureCreated();

            if (!context.Region.Any())
            {
                Municipio municipio = new Municipio()
                {
                    Name = "Medellín",
                    status = 1
                };

                Region region = new Region()
                {
                    Municipios = new List<Municipio> {municipio},
                    Name = "Andina"
                };

                context.SaveChangesAsync(); 
            }


        }
    }
}
