using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Data.Entities
{
    public class RegionMunicipio
    {
        public int MunicipioId { get; set; }
        public Municipio Municipio { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
