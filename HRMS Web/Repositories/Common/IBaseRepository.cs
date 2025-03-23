using System.Linq.Expressions;

namespace HRMS_Web.Repositories.Common
{
    public interface IBaseRepository<T> where T:class//(generic interface class as T)
    {
        //CRUD process will be written in here
        

        //T will be common for all entity 
        void Create (T entity);//Create
        IEnumerable<T> GetAll (Expression <Func<T,bool>> expression);//Retrieve (Expression is for filtering)

        void Update(T entity);//Update
        void Delete (T entity);//Delete
        
    }
}
