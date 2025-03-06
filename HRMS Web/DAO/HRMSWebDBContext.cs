using HRMS_Web.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HRMS_Web.DAO
{
    public class HRMSWebDBContext:DbContext
    {
        public HRMSWebDBContext(DbContextOptions<HRMSWebDBContext> options):base(options)//constructor overloading (making two class with same name) and Dependency Injection in ASP.NET Core (Passing DbContextOptions<T>)
        {

        }
        // register all the Data Model as DBSet
        public DbSet<PositionEntity> Employees { get; set; }//DbSet is a collection of entities that can be queried from the database
        
  

    }
}
