using HRMS_Web.DAO;
using HRMS_Web.Models.DataModels;
using HRMS_Web.Models.ViewModels;
using HRMS_Web.Utilities;
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
        public async Task<IActionResult> Entry(PositionViewModel position_model)
        {
            try
            {
                //DTO (data transfer object) [to change data from viewmodel to datamodel]
                PositionEntity positionEntity = new PositionEntity()
                {
                    Id = Guid.NewGuid().ToString(),//for PK auto increment
                    Name = position_model.Name,//from form input
                    Description = position_model.Description,// from form input
                    Level = position_model.Level,//from form input
                    CreatedAt = DateTime.Now,
                    CreatedBy = "admin",
                    IsActive = true,
                    Ip = await NetworkHelper.GetIPAddress()
                };
                _dbcontext.Positions.Add(positionEntity);//adding the record to the context [Postions is from DBContext]
                _dbcontext.SaveChanges();//saving the data to database in here

                //Message to show success 
                TempData["Msg"] = "New Position is saved successfully!";
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
            IList<PositionViewModel> positionViews = _dbcontext.Positions
       .Where(w => w.IsActive==true)
       .OrderBy(o => o.CreatedAt)
       .Select(s => new PositionViewModel
       {
           Id = s.Id,
           Name = s.Name,
           Description = s.Description,
           Level = s.Level ?? 0 // Avoids null reference issue
           
       })
       .ToList(); // LINQ query with PositionViewModel as the result object

            return View(positionViews);// this will return the list of positions (without adding positionViews will return null in Model of view page)
        }

        public IActionResult Edit(string id)
        {
            PositionViewModel positionView = _dbcontext.Positions//similar to list 
                .Where(w=>w.IsActive && w.Id == id)
                .Select(s=>new PositionViewModel
            { 
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Level = s.Level ?? 0
            }).FirstOrDefault();
            return View(positionView);

        }
        public async Task<IActionResult> Update(PositionViewModel positionVM)
        {
            try
            {
                PositionEntity currentPosition = _dbcontext.Positions.Where(w => w.IsActive && w.Id == positionVM.Id).FirstOrDefault();//retrieve selected data
                if(currentPosition is not null)
                    {
                    currentPosition.Name = positionVM.Name;
                    currentPosition.Description = positionVM.Description;
                    currentPosition.Level = positionVM.Level;
                    currentPosition.UpdatedAt = DateTime.Now;
                    currentPosition.UpdatedBy = "admin";
                    currentPosition.Ip = await NetworkHelper.GetIPAddress();
                    
                    _dbcontext.Positions.Update(currentPosition);//update the record in the context
                    _dbcontext.SaveChanges();//saving the data in database
                    //Message to show success 
                    TempData["Msg"] = "This Position is updated successfully!";
                    //check if error is occured
                    TempData["IsErrorOccur"] = false;
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "There are some errors in saving the record!";
                //check if error is occured
                TempData["IsErrorOccur"] = true;

            }
            return RedirectToAction("List");
        }

        // /position/delete?id=@item.Id
        public IActionResult Delete(string id)//for primary key
        {
            try
            {
                PositionEntity positionEntity = _dbcontext.Positions.Where(w => w.Id == id).FirstOrDefault();//retrive the selected record from database
                if (positionEntity != null)
                {
                    positionEntity.IsActive = false;
                    _dbcontext.Positions.Update(positionEntity);//update the record in the context
                    _dbcontext.SaveChanges();//saving the data in database
                    //Message to show success 
                    TempData["Msg"] = "This Position is deleted successfully!";
                    //check if error is occured
                    TempData["IsErrorOccur"] = false;
                }
            }
            catch (Exception)
            {
                TempData["Msg"] = "There are some errors in deleting the record!";
                //check if error is occured
                TempData["IsErrorOccur"] = true;
                
            }
            return RedirectToAction("List");
        }
    }
}
