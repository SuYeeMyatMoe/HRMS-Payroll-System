using HRMS_Web.DAO;
using HRMS_Web.Models.DataModels;
using HRMS_Web.Repositories.Common;

namespace HRMS_Web.Repositories.Domain
{
    public class EmployeeRepository : BaseRepository<EmployeeEntity>, IEmployeeRepository
    {
        private readonly HRMSWebDBContext _dBContext;

        public EmployeeRepository(HRMSWebDBContext dBContext) : base(dBContext)
        {
            this._dBContext = dBContext;
        }
    }
}
