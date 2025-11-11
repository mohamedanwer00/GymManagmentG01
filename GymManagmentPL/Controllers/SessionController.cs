using GymManagementSystemBLL.View_Models.SessionVm;
using GymManagmentBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionServices _sessionServices;

        public SessionController(ISessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }
        public ActionResult Index()
        {
            var sessions = _sessionServices.GetAllSessions();
            return View(sessions);
        }

        public ActionResult Details(int  id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionServices.GetSessionDetails(id);
            if(session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        public ActionResult Create()
        {
            LoadDropDowns();

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel CreateSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDowns();
                return View(CreateSession);
            }

            var result =_sessionServices.CreateSession(CreateSession);

            if (result)
            {
                TempData["SuccessMessage"] = "Session Created Succesfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                LoadDropDowns(); 
                TempData["ErrorMessage"] = "Srssion Failed To Create";
                return View(CreateSession);

            }

        }

        #region Helper
        private void LoadDropDowns()
        {
            var categories = _sessionServices.GetCategoriesForDropdown();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            var trainers = _sessionServices.GetTrainersForDropdown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        } 
        #endregion

    }
}
