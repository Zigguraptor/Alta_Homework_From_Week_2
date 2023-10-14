using System.ComponentModel.DataAnnotations;

namespace Alta_Homework_Week_2.WebApi.DAL.Entities
{
    public class JobTitleEntity
    {
        [Key] public required string Title { get; set; }
    }
}
