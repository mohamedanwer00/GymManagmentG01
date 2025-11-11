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

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionServices.GetSessionDetails(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        #region create
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

            var result = _sessionServices.CreateSession(CreateSession);

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
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionServices.GetSessionToUpdate(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            LoadTrainersDropDown();
            return View(session);
        }

        [HttpPost]
        public ActionResult Edit(int id,UpdateSessionViewModel UpdatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadTrainersDropDown();
                return View(UpdatedSession);
            }

                var result = _sessionServices.UpdateSession(id,UpdatedSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update session. Please try again.";
                LoadTrainersDropDown();
                return View(UpdatedSession);
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionServices.GetSessionDetails(id);
            if (session ==null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _sessionServices.DeleteSession(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Deleted Succesfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed To Delete";
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion

        #region Helper
        private void LoadDropDowns()
        {
            var categories = _sessionServices.GetCategoriesForDropdown();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            var trainers = _sessionServices.GetTrainersForDropdown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }

        private void LoadTrainersDropDown()
        {
            var trainers = _sessionServices.GetTrainersForDropdown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        #endregion

    }
}
