using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.WebUI.Models
{
    public class MunicipioListViewModel
    {
        public IEnumerable<Municipio> Municipios { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
