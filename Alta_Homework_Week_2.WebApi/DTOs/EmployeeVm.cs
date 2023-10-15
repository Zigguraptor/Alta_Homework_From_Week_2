using Alta_Homework_Week_2.WebApi.Common.Mappings;
using Alta_Homework_Week_2.WebApi.DAL.Entities;
using AutoMapper;

namespace Alta_Homework_Week_2.WebApi.DTOs;

public class EmployeeVm : IMappingSource
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Patronymic { get; set; }
    public required string JobTitle { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap(typeof(EmployeeEntity), GetType());
}
