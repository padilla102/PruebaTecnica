using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Domain.Abstract
{
    public interface IRegionRepository
    {
        IEnumerable<Region> List();
        Region Get(int id);
        void Save(Region municipio);
        Region Delete(int id);

    }
}
