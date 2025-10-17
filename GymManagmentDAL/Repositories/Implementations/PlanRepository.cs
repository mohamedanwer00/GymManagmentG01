using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Implementations
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;

        //private readonly GymDbContext _dbContext = new GymDbContext();
        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PlanReposatory> GetAllPlans()=> _dbContext.Plans.ToList();

        public PlanReposatory? GetPlanById(int id)=> _dbContext.Plans.Find(id);

        public int Update(PlanReposatory plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
