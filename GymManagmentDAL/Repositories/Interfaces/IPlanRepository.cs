using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        //GetAllPlans
        IEnumerable<Plan> GetAllPlans();
        //GetPlanById
        Plan? GetPlanById(int id);
        //AddPlan
        int Add(Plan plan);
        //UpdatePlan
        int Update(Plan plan);
        //DeletePlan
        int Remove(int id);
    }
}
