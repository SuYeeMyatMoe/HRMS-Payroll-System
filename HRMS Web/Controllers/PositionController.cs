using Microsoft.AspNetCore.Mvc;

namespace HRMS_Web.Controllers
{
    public class PositionController : Controller
    {
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
    }
}
