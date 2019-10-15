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


            //builder.Entity<Municipio>()
            //    .HasOne<Region>(s => s.Region)
            //    .WithMany(g => g.Municipios)
            //    .HasForeignKey(s => s.RegionId);

            builder.Entity<Municipio>()
                .HasOne(pt => pt.Status)
                .WithMany()
                .HasForeignKey(pt => pt.StatusId);

            builder.Entity<RegionMunicipio>()
            .HasKey(pt => new { pt.MunicipioId, pt.RegionId });

            builder.Entity<RegionMunicipio>()
                .HasOne(pt => pt.Municipio)
                .WithMany(p => p.RegionMunicipio)
                .HasForeignKey(pt => pt.MunicipioId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<RegionMunicipio>()
                .HasOne(pt => pt.Region)
                .WithMany(t => t.RegionMunicipio)
                .HasForeignKey(pt => pt.RegionId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        public DbSet<Region> Region { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<RegionMunicipio> RegionMunicipio { get; set; }
    }
}
