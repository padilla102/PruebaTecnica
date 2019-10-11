using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PruebaTecnica.Data.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int status { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
