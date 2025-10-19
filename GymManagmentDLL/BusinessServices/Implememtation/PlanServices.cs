using GymManagmentBLL.BusinessServices.Interfaces;
using GymManagmentBLL.BusinessServices.View_Models.PlanVM;
using GymManagmentDAL.Entities;
using GymManagmentDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.Implememtation
{
    internal class PlanServices : IPlanServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();

            if (plans is null || !plans.Any())
                return [];

            return plans.Select(P => new PlanViewModel
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                Price = P.Price,
                IsActive = P.IsActive,
                DurationDays = P.DurationDays,
            });
        }

        public PlanViewModel? GetPlanDetails(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

            if (plan is null)
                return null;
            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                IsActive = plan.IsActive,
                DurationDays = plan.DurationDays,
            };

        }

        public PlanToUpdateViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || plan.IsActive == false || HasActiveMemberships(PlanId))
                return null;

            return new PlanToUpdateViewModel
            {
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
            };
        }

        public bool ToggleStatus(int planId)
        {
            var planRepository = _unitOfWork.GetRepository<Plan>();
            var plan = planRepository.GetById(planId);

            if (plan is null || HasActiveMemberships(planId))
                return false;

            plan.IsActive = !plan.IsActive == true ? false : true;

            plan.UpdatedAt = DateTime.Now;

            planRepository.Update(plan);
            return _unitOfWork.SaveChanges() > 0;



        }



        public bool UpdatePlan(int planId, PlanToUpdateViewModel planToUpdate)
        {
            var planRepository = _unitOfWork.GetRepository<Plan>();
            var plan = planRepository.GetById(planId);

            if (plan is null || planToUpdate is null)
                return false;

            (plan.Description, plan.DurationDays, plan.Price) =
                (planToUpdate.Description, planToUpdate.DurationDays, planToUpdate.Price);

            plan.UpdatedAt = DateTime.Now;

            try
            {

                planRepository.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;

            }
        }

        #region Helper Methods
        private bool HasActiveMemberships(int planId)
        {
            var activeMemberships = _unitOfWork.GetRepository<Membership>()
                .GetAll(X => X.PlanId == planId && X.Status == "Active");
            return activeMemberships.Any();
        }
        #endregion
    }
}
