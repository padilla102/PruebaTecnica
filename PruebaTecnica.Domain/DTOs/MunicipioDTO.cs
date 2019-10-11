using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Domain.DTOs
{
    public class MunicipioDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int status { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
