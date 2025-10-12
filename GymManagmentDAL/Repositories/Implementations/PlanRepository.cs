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
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAllPlans()=> _dbContext.Plans.ToList();


        public Plan? GetPlanById(int id)=> _dbContext.Plans.Find(id);


        public int Remove(int id)
        {
            var plan = _dbContext.Plans.Find(id);
            if (plan is null)
                return 0;
            _dbContext.Plans.Remove(plan);
            return _dbContext.SaveChanges();
        }

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
