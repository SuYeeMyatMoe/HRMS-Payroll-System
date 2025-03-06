using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_Web.Models.DataModels
{
    [Table("Positions")]
    public class PositionEntity: BaseEntity
    {
        [MaxLength(100)]//length of name is 100
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int? Level { get; set; }
        
    }
}
