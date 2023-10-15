using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Alta_Homework_Week_2.WebApi.DTOs;

namespace Alta_Homework_Week_2.WebApi.Services;

public interface IShiftsRepository
{
    public Task StartShift(int employeeId);
    public Task EndShift(int employeeId);
    public Task<List<ShiftVm>> GetShiftsAsync();
    public Task<List<ShiftVm>> GetShiftsAsync(int employeeId);
    public Task<List<ShiftVm>> GetCurrentShiftsAsync();
    public Task<List<ShiftVm>> GetCurrentShiftsAsync(int employeeId);
}
