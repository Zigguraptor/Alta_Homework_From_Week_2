using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.DAL.Entities
{
    [Index(nameof(EndTime))]
    [Index(nameof(EmployeeId))]
    public class ShiftRecordEntity
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int EmployeeId { get; set; }

        [Required]
        [ForeignKey("EmployeeId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public EmployeeEntity EmployeeEntity { get; set; } = null!;
    }
}
