using Microsoft.AspNetCore.Mvc;

namespace HRMS_Web.Controllers
{
    public class DailyAttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
