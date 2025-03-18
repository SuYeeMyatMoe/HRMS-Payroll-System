using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_Web.Models.DataModels
{
    [Table("Employees")]
    public class EmployeeEntity : BaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required char Gender { get; set; }
        public required DateTime DOB { get; set; }
        public required DateTime DOE { get; set; }
        public DateTime DOR { get; set; }
        public required string Address { get; set; }
        public required Decimal BasicSalary { get; set; }
        public string? Phone { get; set; }
        public required string DepartmentID { get; set; }
        public required string PositionID { get; set; }

    }
}
