using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.WebUI.Models
{
    public class RegionViewModel
    {
        public Region Region { get; set; }
        public Municipio MunicipioAux { get; set; }
        public IEnumerable<Municipio> ListMunicipio { get; set; }
        public int MunicipioId { get; set; }

    }
}
