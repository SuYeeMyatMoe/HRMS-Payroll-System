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
            
                //Message to show success 
                TempData["Msg"] = "New Position is saved successfully!";
                //check if error is occured
                TempData["IsErrorOccur"] = false;

            return RedirectToAction("List");
        }

        //To show success result
        public IActionResult List()
        {
            

            return View(positionViews);// this will return the list of positions (without adding positionViews will return null in Model of view page)
        }

        public IActionResult Edit(string id)
        {

            return View(positionView);

        }

        [HttpPost]
        public async Task<IActionResult> Update(PositionViewModel positionVM)
        {
            
                    //Message to show success 
                    TempData["Msg"] = "This Position is updated successfully!";
                    //check if error is occured
                    TempData["IsErrorOccur"] = false;


            return RedirectToAction("List");
        }

        // /position/delete?id=@item.Id
        public IActionResult Delete(string id)//for primary key
        {
            
                    //Message to show success 
                    TempData["Msg"] = "This Position is deleted successfully!";
                    //check if error is occured
                    TempData["IsErrorOccur"] = false;

            return RedirectToAction("List");
        }
    }
}
