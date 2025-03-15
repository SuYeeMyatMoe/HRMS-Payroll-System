using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_Web.Models.DataModels
{
    [Table("Departments")]
    public class DepartmentEntity:BaseEntity
    {
            [MaxLength(100)]//length of code is 100
            public required string Code { get; set; }
            public required string Description { get; set; }
            public required string ExtensionPhone { get; set; }
        
    }
}
