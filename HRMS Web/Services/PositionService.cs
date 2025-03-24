using HRMS_Web.Models.DataModels;
using HRMS_Web.Models.ViewModels;
using HRMS_Web.UnitOfWork;
using HRMS_Web.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HRMS_Web.Services
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitofwork;


        //CRUD in controller in will be written in here 
        //and store in dbs with the help of unit of work
        public PositionService(IUnitOfWork unitofwork)
        {
            this._unitofwork = unitofwork;
        }
        public async Task Create(PositionViewModel viewModel)
        {
            try
            {
                //DTO (data transfer object) [to change data from viewmodel to datamodel]
                PositionEntity positionEntity = new PositionEntity()
                {
                    Id = Guid.NewGuid().ToString(),//for PK auto increment
                    Name = viewModel.Name,//from form input
                    Description = viewModel.Description,// from form input
                    Level = viewModel.Level,//from form input
                    CreatedAt = DateTime.Now,
                    CreatedBy = "admin",
                    IsActive = true,
                    Ip = await NetworkHelper.GetIPAddress()
                };
                //save with unit of work
                _unitofwork.PositionRepository.Create(positionEntity);
                _unitofwork.Commit();//saving the data to database in here

            }
            catch (Exception e)
            {
                _unitofwork.Rollback();

            }
            //return RedirectToAction("List");
        }

        public void Delete(PositionViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PositionViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public PositionViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PositionViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
