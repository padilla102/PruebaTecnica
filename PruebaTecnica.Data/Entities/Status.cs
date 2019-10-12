using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PruebaTecnica.Data.Entities
{
    public class Status
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
