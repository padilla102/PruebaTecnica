using PruebaTecnica.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Domain.Abstract
{
    public interface IStatusRepository
    {
        IEnumerable<Status> List();
    }
}
