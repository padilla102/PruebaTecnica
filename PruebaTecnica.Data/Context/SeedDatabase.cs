using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            if (!context.Region.Any())
            {
                context.Municipio.AddRange(
                    new Municipio { Name = "Municipio1", status = 1 },
                    new Municipio { Name = "Municipio2", status = 1 },
                    new Municipio { Name = "Municipio3", status = 1 },
                    new Municipio { Name = "Municipio4", status = 1 });



                context.SaveChanges(); 
            }


        }
    }
}
