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
            _dBContext = dBContext;
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
                    DOR = employeeViewModel.DOR ?? null, // Explicit handling of nullable DateTime
                    //to use that syntax, it must be nullable in viewmodel (not just in entity)

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
                await _dBContext.SaveChangesAsync();//save the changes to database
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
            IList<EmployeeViewModel> employeeViews = (from e in _dBContext.Employees
                                                      join p in _dBContext.Positions
                                                      on e.PositionID equals p.Id
                                                      join d in _dBContext.Departments
                                                      on e.DepartmentID equals d.Id
                                                      where e.IsActive && d.IsActive && p.IsActive
                                                      orderby e.CreatedAt ascending
                                                      select new EmployeeViewModel{
                                                          Id=e.Id,
                                                          Code=e.Code,
                                                          Name=e.Name,
                                                          Email=e.Email,
                                                          Gender=e.Gender,
                                                          DOB=e.DOB,
                                                          DOE=e.DOE,
                                                          DOR=e.DOR.Value,//value is used to get the value of nullable datetime
                                                          Address=e.Address,
                                                          BasicSalary=e.BasicSalary,
                                                          Phone=e.Phone,
                                                          DepartmentInfo=d.Description,
                                                          PositionInfo=p.Description
                                                      }).ToList();

            return View(employeeViews);// this will return the list of employee (without adding employeeViews will return null in Model of view page)
        }

        public IActionResult Edit(string id)
        {
            EmployeeViewModel employeeView=_dBContext.Employees.Where(e=>e.IsActive && e.Id==id).Select(e=>new EmployeeViewModel
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Email = e.Email,
                Gender = e.Gender,
                DOB = e.DOB,
                DOE = e.DOE,
                DOR = e.DOR.Value,//value is used to get the value of nullable datetime
                Address = e.Address,
                BasicSalary = e.BasicSalary,
                Phone = e.Phone,
                DepartmentID = e.DepartmentID,
                PositionID = e.PositionID
            }).FirstOrDefault();
           return View();
        }
        public IActionResult Delete(string id)
        {
            try
            {
                EmployeeEntity employeeEntity = _dBContext.Employees.Where(e => e.IsActive && e.Id == id).FirstOrDefault();
                if (employeeEntity is not null)//if it is exist
                {
                    employeeEntity.IsActive = false;
                    _dBContext.Employees.Update(employeeEntity);//update the record in the context
                    _dBContext.SaveChanges();
                    TempData["Msg"] = "Data has been deleted successfully";
                    TempData["IsErrorOccur"] = false;
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "There is some issue in deleting record";
                TempData["IsErrorOccur"] = true;

            }
            return RedirectToAction("List");
        }
    }
    }

