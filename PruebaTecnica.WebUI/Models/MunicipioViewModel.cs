using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.WebUI.Models
{
    public class MunicipioViewModel
    {
        public Municipio Municipio { get; set; }    
        public IEnumerable<Status> ListStatus { get; set; }
        public IEnumerable<Region> ListRegions { get; set; }
    }
}
