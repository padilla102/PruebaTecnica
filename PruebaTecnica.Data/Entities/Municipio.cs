using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PruebaTecnica.Data.Entities
{
    public class Municipio
    {
        public int? Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage ="Nombre es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Estado es obligatorio")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        //[Required(ErrorMessage = "Region es obligatorio")]
        //public int RegionId { get; set; }
        //public Region Region { get; set; }
        public IList<RegionMunicipio> RegionMunicipio { get; set; }
    }
}
