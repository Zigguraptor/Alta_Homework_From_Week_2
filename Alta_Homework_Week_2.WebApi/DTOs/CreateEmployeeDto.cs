using System.ComponentModel.DataAnnotations;
using Alta_Homework_Week_2.WebApi.Common.Mappings;
using Alta_Homework_Week_2.WebApi.DAL.Entities;
using AutoMapper;

namespace Alta_Homework_Week_2.WebApi.DTOs
{
    public class CreateEmployeeDto : IMappingSource
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = $"Поле {nameof(Name)} должно быть заполнено")]
        public required string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = $"Поле {nameof(Surname)} должно быть заполнено")]
        public required string Surname { get; set; }

        [MinLength(1)]
        [RegularExpression(@"\S", ErrorMessage = $"Поле {nameof(Patronymic)} не может состоять только из пробелов")]
        public string? Patronymic { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = $"Поле {nameof(JobTitle)} должно быть заполнено")]
        public required string JobTitle { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEmployeeDto, EmployeeEntity>()
                .ForMember(employee => employee.Name,
                    opt =>
                        opt.MapFrom(dto => dto.Name))
                .ForMember(employee => employee.Surname,
                    opt =>
                        opt.MapFrom(dto => dto.Surname))
                .ForMember(employee => employee.Patronymic,
                    opt =>
                        opt.MapFrom(dto => dto.Patronymic))
                .ForMember(employee => employee.JobTitle,
                    opt =>
                        opt.MapFrom(dto => dto.JobTitle));
        }
    }
}
