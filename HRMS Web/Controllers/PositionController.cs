using HRMS_Web.DAO;
using HRMS_Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_Web.Controllers
{
    public class PositionController : Controller
    {
        public readonly HRMSWebDBContext _dbcontext;//Dependency injection dbContext
        public PositionController(HRMSWebDBContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(PositionViewModel model)
        {
            //DTO (data transfer object) [to change data from viewmodel to datamodel]
            return View();
        }
    }
}
