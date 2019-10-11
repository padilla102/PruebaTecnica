using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Domain.Abstract
{
    public interface IMunicipioRepository
    {
        IEnumerable<Municipio> List();
        Municipio Get(int id);
        bool Create(Municipio municipio);
        bool Edit(Municipio municipio);
        bool Delete(int id);

    }
}
