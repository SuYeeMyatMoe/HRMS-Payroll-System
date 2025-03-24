using HRMS_Web.Repositories.Domain;

namespace HRMS_Web.UnitOfWork
{
    //for rollback and commit in DBMS transaction management
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();

        //to connect each repository with unit of work to save in dbs
        IPositionRepository PositionRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IEmployeeRepository EmployeeRepositiory { get; }
    }
}
