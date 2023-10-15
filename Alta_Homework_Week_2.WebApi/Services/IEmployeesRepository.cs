using Alta_Homework_Week_2.WebApi.DTOs;

namespace Alta_Homework_Week_2.WebApi.Services;

public interface IEmployeesRepository
{
    public Task<List<EmployeeVm>> GetEmployees();
    public Task<EmployeeVm> GetEmployee(int id);
    public Task<int> AddNewEmployee(CreateEmployeeDto createEmployeeDto);
    public Task DeleteEmployee(int id);
    public Task UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
}
