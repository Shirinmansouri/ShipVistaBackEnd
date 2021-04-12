using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plants.Domain.Entities;
using Plants.Domain.IRepository;
using Plants.EF;

namespace Plants.Persistance.Repository
{
    public class PlantsRepository : GenericRepository<Plants.Domain.Entities.Plant, PlantsDbContext>, IPlantsRepository
    {
        public static List<Plant> LstPlants { get ; set; }
        public PlantsRepository(PlantsDbContext context) : base(context)
        {
            if (LstPlants == null)
                LstPlants = new List<Plant>();
        }
        public override async Task Add(Plant entity)
        {
            if (LstPlants == null) LstPlants = new List<Plant>();
            entity.Id = LstPlants.Count + 1;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            await Task.Run(() => LstPlants.Add(entity));

        }
        public override async Task<IEnumerable<Plant>> GetAll()
        {
            return await Task.Run(() => LstPlants);
        }
        public override async Task Update(Plant entity)
        {
            var indexOf = LstPlants.IndexOf(LstPlants.Find(a => a.Id == entity.Id));
            await Task.Run(() => LstPlants[indexOf] = entity);


        }
        public async Task<Plant> GetById(long id)
        {
            return await Task.Run(() => LstPlants.Find(a => a.Id == id));
        }

    }


}
