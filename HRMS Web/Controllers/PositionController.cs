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
            return RedirectToAction("List");
        }

        //To show success result
        public IActionResult List()
        {
            

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

        [HttpPost]
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
