using Microsoft.AspNetCore.Mvc;

namespace HRMS_Web.Controllers
{
    public class DepartmentController : Controller
    {
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(DepartmentController departmentController)
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
