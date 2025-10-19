using GymManagmentBLL.BusinessServices.View_Models.PlanVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.Interfaces
{
    internal interface IPlanServices
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanDetails(int PlanId);
        PlanToUpdateViewModel? GetPlanToUpdate(int PlanId);
        bool UpdatePlan(int planId,PlanToUpdateViewModel planToUpdate);
        bool ToggleStatus(int planId);

    }
}
