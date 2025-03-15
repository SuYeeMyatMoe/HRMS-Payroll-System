using HRMS_Web.DAO;
using HRMS_Web.Models.DataModels;
using HRMS_Web.Models.ViewModels;
using HRMS_Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_Web.Controllers
{
    public class DepartmentController : Controller
    {
        public readonly HRMSWebDBContext _dbcontext;//Dependency injection dbContext
        public DepartmentController(HRMSWebDBContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entry(DepartmentViewModel department_model)
        {
            try
            {
                //DTO (data transfer object) [to change data from viewmodel to datamodel]
                DepartmentEntity departmentEntity = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString(),//for PK auto increment
                    Code = department_model.Code,//from form input
                    Description = department_model.Description,// from form input
                    ExtensionPhone = department_model.ExtensionPhone,//from form input
                    CreatedAt = DateTime.Now,
                    CreatedBy = "admin",
                    IsActive = true,
                    Ip = await NetworkHelper.GetIPAddress()
                };
                _dbcontext.Departments.Add(departmentEntity);//adding the record to the context [Departments is from DBContext]
                _dbcontext.SaveChanges();//saving the data to database in here

                //Message to show success 
                TempData["Msg"] = "New Department is saved successfully!";
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

        //To show success result
        public IActionResult List()
        {
            IList<DepartmentViewModel> departmentViews = _dbcontext.Departments
       .OrderBy(o => o.CreatedAt)
       .Select(s => new DepartmentViewModel
       {
           Id = s.Id,
           Code = s.Code,
           Description = s.Description,
           ExtensionPhone = s.ExtensionPhone 
       })
       .ToList(); // LINQ query with PositionViewModel as the result object

            return View(departmentViews);// this will return the list of positions (without adding positionViews will return null in Model of view page)
        }
        

    }
}
