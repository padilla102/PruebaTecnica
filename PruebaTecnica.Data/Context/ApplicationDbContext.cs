using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PruebaTecnica.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Municipio>()
            //    .HasOne<Region>(s => s.Region)
            //    .WithMany(g => g.Municipios)
            //    .HasForeignKey(s => s.RegionId);
        }

        public DbSet<Region> Region { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
