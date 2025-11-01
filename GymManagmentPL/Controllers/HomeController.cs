using GymManagmentBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService) 
        {
            _analyticsService = analyticsService;
        }
        //BaseURL/Home/Index
        public IActionResult Index()
        {
            var data = _analyticsService.GetHomeAnalyticsService();

            //return View();
            return View(data);
            //return View("mohamed");
            //return View("mohamed", data);
        }
    }
}
