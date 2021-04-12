using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plants.Domain.Entities;

namespace Plants.Domain.IRepository
{
    public interface IPlantsRepository : IGenericRepository<Plants.Domain.Entities.Plant>
    {
        Task<Plant> GetById(long id);
    }
}
