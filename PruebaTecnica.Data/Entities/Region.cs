using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PruebaTecnica.Data.Entities
{
    public class Region
    {
        public int? Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Name { get; set; }
        //public List<Municipio> Municipios { get; set; }
        public IList<RegionMunicipio> RegionMunicipio { get; set; }
    }
}
