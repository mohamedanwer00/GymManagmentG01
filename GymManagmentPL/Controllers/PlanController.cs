using GymManagmentBLL.BusinessServices.Interfaces;
using GymManagmentBLL.BusinessServices.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanServices _planService;

        public PlanController(IPlanServices planService) 
        {
            _planService = planService;
        }
        //Get:Index
        public ActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }
        //Get:Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan ID.";
                return RedirectToAction(nameof(Index));
            }
               
            var plan = _planService.GetPlanDetails(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
              
            return View(plan);
        }
        //Det:Data To Update
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);

            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, PlanToUpdateViewModel UpdatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Please correct the errors and try again.");
                return View(UpdatedPlan);
            }

            var result = _planService.UpdatePlan(id, UpdatedPlan);
            if (result)
            {
                TempData["SuccessMessage"] = "Plan updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while updating the plan. Please try again.";
            }
            return RedirectToAction(nameof(Index));

        }
        //Post:Submit Update
        //Post:Activate
        [HttpPost]
        public ActionResult Activate(int id)
        {
            var result = _planService.ToggleStatus(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Plan Toggle Succesfuly";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed To Toggle";
            }
            return RedirectToAction(nameof(Index));

        }


    }
}
