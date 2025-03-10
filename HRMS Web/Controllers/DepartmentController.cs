using Microsoft.AspNetCore.Mvc;

namespace HRMS_Web.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Entry()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
