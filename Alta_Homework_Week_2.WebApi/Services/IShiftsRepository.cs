namespace Alta_Homework_Week_2.WebApi.Services;

public interface IShiftsRepository
{
    public Task StartShift(int employeeId);
    public Task EndShift(int employeeId);
}
