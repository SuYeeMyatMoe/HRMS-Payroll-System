using HRMS_Web.DAO;
using HRMS_Web.Models.DataModels;
using HRMS_Web.Models.ViewModels;
using HRMS_Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HRMSWebDBContext _dBContext;

        public EmployeeController(HRMSWebDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        [HttpGet]
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
        public async Task<IActionResult> Entry(EmployeeViewModel employeeViewModel)
        {
            try
            {
                EmployeeEntity employeeEntity = new EmployeeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = employeeViewModel.Code,
                    Name = employeeViewModel.Name,
                    Email = employeeViewModel.Email,
                    DOB = employeeViewModel.DOB,
                    DOE = employeeViewModel.DOE,
                    DOR = employeeViewModel.DOR,
                    Address = employeeViewModel.Address,
                    BasicSalary = employeeViewModel.BasicSalary,
                    Phone = employeeViewModel.Phone,
                    DepartmentID = employeeViewModel.DepartmentID,
                    PositionID = employeeViewModel.PositionID,
                    Gender = employeeViewModel.Gender,
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedAt = DateTime.Now,
                    Ip = await NetworkHelper.GetIPAddress()
                };//DTO process which is changing from viewmodel to entity(datamodel)
                _dBContext.Employees.Add(employeeEntity);//add entity to dbcontext
                _dBContext.SaveChangesAsync();//save the changes to database
                //Message to show success 
                TempData["Msg"] = "New Employee is saved successfully!";
                //check if error is occured
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "There are some errors in saving the record!";
                //check if error is occured
                TempData["IsErrorOccur"] = true;

            }
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            IList<EmployeeViewModel> employeeViews = _dBContext.Employees
        .Where(w => w.IsActive)//same with  == true
        .OrderBy(o => o.CreatedAt)
        .Select(s => new EmployeeViewModel
        {
            Id = s.Id,
            Code = s.Code,
            Name = s.Name,
            Email = s.Email,
            Gender=s.Gender,
            DOB = s.DOB,
            BasicSalary = s.BasicSalary
        })
        .ToList(); // LINQ query with PositionViewModel as the result object

            return View(employeeViews);// this will return the list of positions (without adding positionViews will return null in Model of view page)
        }
    }
    }

