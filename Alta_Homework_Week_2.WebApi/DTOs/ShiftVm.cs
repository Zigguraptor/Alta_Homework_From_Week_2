using System.Text.Json.Serialization;
using Alta_Homework_Week_2.WebApi.Common.Mappings;
using Alta_Homework_Week_2.WebApi.DAL.Entities;
using AutoMapper;

namespace Alta_Homework_Week_2.WebApi.DTOs;

public class ShiftVm : IMappingSource
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    [JsonPropertyName("employee")] public EmployeeVm EmployeeVm { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ShiftRecordEntity, ShiftVm>()
            .ForMember(vm => vm.Id, o =>
                o.MapFrom(sr => sr.Id))
            .ForMember(vm => vm.StartTime, o =>
                o.MapFrom(sr => sr.StartTime))
            .ForMember(vm => vm.EndTime, o =>
                o.MapFrom(sr => sr.EndTime))
            .ForMember(vm => vm.EmployeeVm, o =>
                o.MapFrom(sr => sr.EmployeeEntity));
    }
}
