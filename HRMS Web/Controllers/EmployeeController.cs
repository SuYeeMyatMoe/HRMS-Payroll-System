using HRMS_Web.DAO;
using HRMS_Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HRMSWebDBContext _dBContext;

        public EmployeeController(HRMSWebDBContext dBContext)
        {
            this._dBContext = dBContext;
        }
    
        public IActionResult Entry()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()//create object of EmployeeViewModel
            {
                DepartmentViewModels = _dBContext.Departments
                .Where(w => w.IsActive)
                .Select(s => new DepartmentViewModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    Description = s.Description
                }).ToList(),

                PositionViewModels = _dBContext.Positions
                .Where(w => w.IsActive)
                .Select(s => new PositionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                }).ToList()
            };
            return View(employeeViewModel);
        }
        [HttpPost]
        public IActionResult Entry(EmployeeViewModel employeeViewModel)
        {
            
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
