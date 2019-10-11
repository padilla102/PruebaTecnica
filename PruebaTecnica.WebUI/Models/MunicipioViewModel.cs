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
        public IEnumerable<SelectListItem> ListStatus {
            get
            {
                return Enum.GetNames(typeof(Status)).Select(e => new SelectListItem() { Text = e, Value = e });
            }
        }
    }

    public enum Status
    {
        Activo = 1,
        Inactivo = 0
    }
}
