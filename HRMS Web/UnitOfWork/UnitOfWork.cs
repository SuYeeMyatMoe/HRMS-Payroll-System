using HRMS_Web.DAO;

namespace HRMS_Web.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRMSWebDBContext _dBContext;

        public UnitOfWork(HRMSWebDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
