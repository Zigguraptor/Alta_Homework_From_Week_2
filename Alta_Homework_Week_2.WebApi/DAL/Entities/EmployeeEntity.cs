using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.DAL.Entities
{
    public class EmployeeEntity
    {
        [Key] public int Id { get; set; }
        [Required] public required string Name { get; set; }
        [Required] public required string Surname { get; set; }
        public string? Patronymic { get; set; }

        public required string JobTitle { get; set; }

        [Required]
        [ForeignKey("JobTitle")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public JobTitleEntity Job { get; set; } = null!;

        public IEnumerable<ShiftRecordEntity> ShiftRecord { get; set; } = null!;
    }
}
