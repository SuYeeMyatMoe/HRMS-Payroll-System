using HRMS_Web.DAO;
using System.Linq.Expressions;

namespace HRMS_Web.Repositories.Common
{
    //To partially implement IBaseRepository interface in this abstract class
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly HRMSWebDBContext _dBContext;

        public BaseRepository(HRMSWebDBContext dBContext)
        {          
            this._dBContext = dBContext;
        }
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
