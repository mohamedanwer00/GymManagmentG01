using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        //GetAllPlans
        IEnumerable<PlanReposatory> GetAllPlans();
        //GetPlanById
        PlanReposatory? GetPlanById(int id);

        //update
        int Update(PlanReposatory plan);
    }
}
