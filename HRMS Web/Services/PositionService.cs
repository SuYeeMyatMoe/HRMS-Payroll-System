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
        private async Task<string> GetIPAsync()
        {
            return await NetworkHelper.GetIPAddress();
        }
        //create
        public void Create(PositionViewModel viewModel)
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
                    Ip = GetIPAsync().GetAwaiter().GetResult()// Prevents deadlock (due to .Result)
                };
                //save with unit of work
                _unitofwork.PositionRepository.Create(positionEntity);
                _unitofwork.Commit();//saving the data to database in here

            }
            catch (Exception e)
            {
                _unitofwork.Rollback();

            }
            
        }

        //delete
        public void Delete(string id)
        {
            try
            {
                PositionEntity positionEntity = _unitofwork.PositionRepository.GetAll(w => w.Id == id).FirstOrDefault();//retrive the selected record from database
                if (positionEntity != null)
                {
                    positionEntity.IsActive = false;
                    _unitofwork.PositionRepository.Delete(positionEntity);
                    _unitofwork.Commit();
                }
            }
            catch (Exception)
            {
                _unitofwork.Rollback();
                
            }
        }

        //Retrieve all (for list) 
        public IEnumerable<PositionViewModel> GetAll()
        {
            return _unitofwork.PositionRepository.GetAll
        (w => w.IsActive == true)
       .OrderBy(o => o.CreatedAt)
       .Select(s => new PositionViewModel
       {
           Id = s.Id,
           Name = s.Name,
           Description = s.Description,
           Level = s.Level ?? 0 // Avoids null reference issue

       })
       .ToList(); // LINQ query with PositionViewModel as the result object
        }

        //Retrieve by id (for edit)
        public PositionViewModel GetById(string id)
        {
            return _unitofwork.PositionRepository//similar to list 
                .GetAll(w => w.IsActive && w.Id == id)
                .Select(s => new PositionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Level = s.Level ?? 0
                }).FirstOrDefault();
            
        }

        //update
        public void Update(PositionViewModel viewModel)
        {
            try
            {
                PositionEntity currentPosition = _unitofwork.PositionRepository.GetAll(w => w.IsActive && w.Id == viewModel.Id).FirstOrDefault();//retrieve selected data
                if (currentPosition is not null)
                {
                    currentPosition.Name = viewModel.Name;
                    currentPosition.Description = viewModel.Description;
                    currentPosition.Level = viewModel.Level;
                    currentPosition.UpdatedAt = DateTime.Now;
                    currentPosition.UpdatedBy = "admin";
                    currentPosition.Ip = GetIPAsync().GetAwaiter().GetResult();// Prevents deadlock 
                    _unitofwork.PositionRepository.Update(currentPosition);
                    _unitofwork.Commit();                 
                }
            }
            catch (Exception e)
            {
                _unitofwork.Rollback();
            }
        }
    }
}
