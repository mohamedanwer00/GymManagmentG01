
namespace GymManagmentBLL.BusinessServices.Interfaces
{
    internal interface IPlanServices
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanDetails(int PlanId);
        PlanToUpdateViewModel? GetPlanToUpdate(int PlanId);
        bool UpdatePlan(int planId, PlanToUpdateViewModel planToUpdate);
        bool ToggleStatus(int planId);

    }
}
