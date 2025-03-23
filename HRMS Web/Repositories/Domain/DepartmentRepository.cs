using HRMS_Web.DAO;
using HRMS_Web.Models.DataModels;
using HRMS_Web.Repositories.Common;

namespace HRMS_Web.Repositories.Domain
{
    public class DepartmentRepository : BaseRepository<DepartmentEntity>, IDepartmentRepository
    {
        private readonly HRMSWebDBContext _dBContext;

        public DepartmentRepository(HRMSWebDBContext dBContext) : base(dBContext)
        {
            this._dBContext = dBContext;
        }
    }
}
