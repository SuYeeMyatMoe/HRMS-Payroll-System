using HRMS_Web.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRMS_Web.DAO
{
    public class HRMSWebDBContext:IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public HRMSWebDBContext(DbContextOptions<HRMSWebDBContext> options):base(options)//constructor overloading (making two class with same name) and Dependency Injection in ASP.NET Core (Passing DbContextOptions<T>)
        {

        }
        // register all the Data Model as DBSet
        public DbSet<PositionEntity> Positions { get; set; }//DbSet is a collection of entities that can be queried from the database
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
  

    }
}
