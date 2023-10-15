using Alta_Homework_Week_2.WebApi.DAL.Entities;

namespace Alta_Homework_Week_2.WebApi.Services;

public interface IShiftsRepository
{
    public Task StartShift(int employeeId);
    public Task EndShift(int employeeId);
    public Task<List<ShiftRecordEntity>> GetShiftsAsync();
    public Task<List<ShiftRecordEntity>> GetShiftsAsync(int employeeId);
    public Task<List<ShiftRecordEntity>> GetCurrentShiftsAsync();
    public Task<List<ShiftRecordEntity>> GetCurrentShiftsAsync(int employeeId);
}
