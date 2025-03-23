namespace HRMS_Web.UnitOfWork
{
    //for rollback and commit in DBMS transaction management
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
