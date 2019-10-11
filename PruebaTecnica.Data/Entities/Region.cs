using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Data.Entities
{
    public class Region
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Municipio> Municipios { get; set; }
    }
}
