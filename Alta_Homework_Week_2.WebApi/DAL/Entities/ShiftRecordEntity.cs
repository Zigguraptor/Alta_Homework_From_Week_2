using System.ComponentModel.DataAnnotations;

namespace Alta_Homework_Week_2.WebApi.DAL.Entities
{
    public class ShiftRecordEntity
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Required] public required EmployeeEntity EmployeeEntity { get; set; }
    }
}
