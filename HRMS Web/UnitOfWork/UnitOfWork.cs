using HRMS_Web.DAO;
using HRMS_Web.Repositories.Domain;

namespace HRMS_Web.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRMSWebDBContext _dBContext;

        public UnitOfWork(HRMSWebDBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        //Position
        private IPositionRepository positionRepository;//variable name for position repository

        public IPositionRepository PositionRepository
        {
            get {
                return positionRepository = positionRepository ??new PositionRepository(_dBContext);//if the class is exist then obj is created  
            }
        }

        //Department
        private IDepartmentRepository departmentRepository;
        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                return departmentRepository = departmentRepository ?? new DepartmentRepository(_dBContext);
            }
        }

        //Employee
        private IEmployeeRepository employeeRepository;
        public IEmployeeRepository EmployeeRepositiory
        {
            get
            {
                return employeeRepository=employeeRepository??new EmployeeRepository(_dBContext);
            }
        }

        public void Commit()
        {
            _dBContext.SaveChanges();//save in dbs
        }

        public void Rollback()
        {
            _dBContext.Dispose();
        }
    }
}
