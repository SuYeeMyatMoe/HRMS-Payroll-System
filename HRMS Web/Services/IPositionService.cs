using HRMS_Web.Models.ViewModels;

namespace HRMS_Web.Services
{
    public interface IPositionService
    {
        //CRUD process

        //create
        void Create(PositionViewModel viewModel);//service will work with viewmodel

        //read
        IEnumerable<PositionViewModel> GetAll();
        PositionViewModel GetById(int id);

        //update
        void Update(PositionViewModel viewModel);

        //delete
        void Delete(PositionViewModel viewModel);

    }
}
