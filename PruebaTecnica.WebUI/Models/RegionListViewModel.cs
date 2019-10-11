using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.WebUI.Models
{
    public class RegionListViewModel
    {
        public IEnumerable<Region> Regions { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
