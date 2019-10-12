using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Data.Context;
using PruebaTecnica.Data.Entities;
using PruebaTecnica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PruebaTecnica.Domain.Implements
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext context;
        
        public StatusRepository(IServiceProvider service)
        {
            this.context = service.GetRequiredService<ApplicationDbContext>();
        }
        public IEnumerable<Status> List()
        {
            try
            {
                return context.Status.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
