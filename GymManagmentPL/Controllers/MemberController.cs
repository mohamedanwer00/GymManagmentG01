using GymManagmentBLL.BusinessServices.Interfaces;
using GymManagmentBLL.BusinessServices.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberServices _memberServices;

        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        public ActionResult Index()
        {
            var members = _memberServices.GetAllMembers();
            return View(members);
        }

        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));
            var member = _memberServices.GetMemberDetails(id);
            if (member == null)
                return RedirectToAction(nameof(Index));
            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));
            var healthRecordData = _memberServices.GetMemberHealthRecord(id);
            if (healthRecordData == null)
                return RedirectToAction(nameof(Index));
            return View(healthRecordData);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View(nameof(Create), createMember);
            }

            bool result=_memberServices.CreateMember(createMember);
            if (result)
            {
                TempData["SuccessMessage"] = "Member created successfully.";
            }

            else
            {
                TempData["ErrorMessage"] = "An error occurred while creating the member. Please try again.";
            }
            return RedirectToAction(nameof(Index));


        }


        //Get
        #region member edit
        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberServices.GetMemberDetailsToUpdate(id);

            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id, MemberToUpdateViewModel UpdatedMember)
        {

            if (!ModelState.IsValid)
            {
                return View(UpdatedMember);
            }
            var result = _memberServices.UpdateMember(id, UpdatedMember);
            if (result)
            {
                TempData["SuccessMessage"] = "Member updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while updating the member. Please try again.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Delete Member
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberServices.GetMemberDetails(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            return View("Delate");
        }

        [HttpPost]
        public ActionResult DelateConfirmed([FromForm]int id)
        {
            var result = _memberServices.RemoveMember(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Member deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the member. Please try again.";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

    }
}
