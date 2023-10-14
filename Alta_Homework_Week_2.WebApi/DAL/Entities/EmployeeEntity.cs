using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alta_Homework_Week_2.WebApi.DAL.Entities
{
    public class EmployeeEntity
    {
        [Key] public int Id { get; set; }
        [Required] public required string Name { get; set; }
        [Required] public required string Surname { get; set; }
        public string? Patronymic { get; set; }
        [Required] public required string JobTitle { get; set; }

        [ForeignKey("JobTitle")] public JobTitleEntity Job { get; set; } = null!;
        public IEnumerable<ShiftRecord> ShiftRecord { get; set; } = null!;
    }
}
