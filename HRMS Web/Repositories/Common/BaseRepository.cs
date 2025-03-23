using HRMS_Web.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HRMS_Web.Repositories.Common
{
    //To partially implement IBaseRepository interface in this abstract class
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly HRMSWebDBContext _dBContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(HRMSWebDBContext dBContext)
        {          
            this._dBContext = dBContext;
            _dbSet = _dBContext.Set<T>();
        }
        public void Create(T entity)
        {
            _dBContext.Add<T>(entity);
        }

        public void Delete(T entity)
        {
            _dBContext.Update<T>(entity);//since delete is also to update IsActive
        }

        public IEnumerable<T> GetAll(Expression<Func<T,bool>>expression)
        {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();//To retrieve listing with AsNoTracking
            //AsNoTracking() for read-only operations to optimize performance.
        }

        public void Update(T entity)
        {
            _dBContext.Update<T>(entity);
        }
    }
}
