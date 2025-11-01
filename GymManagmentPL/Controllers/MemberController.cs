using GymManagmentBLL.BusinessServices.Interfaces;
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
            if (id<=0)
                return RedirectToAction(nameof(Index));
            var member=_memberServices.GetMemberDetails(id);
            if (member == null)
                return RedirectToAction(nameof(Index));
            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));
            var healthRecordData = _memberServices.GetMemberHealthRecord(id);
            if(healthRecordData == null)
                return RedirectToAction(nameof(Index));
            return View(healthRecordData);
        }


    }
}
