using Alta_Homework_Week_2.WebApi.DAL.DbContexts;
using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Alta_Homework_Week_2.WebApi.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.Services;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly IEmployeesShiftDbContext _employeesShiftDbContext;
    private readonly IMapper _mapper;

    public EmployeesRepository(IEmployeesShiftDbContext employeesShiftDbContext, IMapper mapper)
    {
        _employeesShiftDbContext = employeesShiftDbContext;
        _mapper = mapper;
    }

    public Task<List<EmployeeVm>> GetEmployees()
    {
        return _employeesShiftDbContext.Employees
            .Select(e => _mapper.Map<EmployeeVm>(e))
            .ToListAsync();
    }

    public async Task<EmployeeVm> GetEmployee(int id)
    {
        var employee = await _employeesShiftDbContext.Employees.FindAsync(id);
        if (employee == null)
            throw new KeyNotFoundException();

        return _mapper.Map<EmployeeVm>(employee);
    }

    public async Task<int> AddNewEmployee(CreateEmployeeDto createEmployeeDto)
    {
        var employee = _mapper.Map<EmployeeEntity>(createEmployeeDto);
        _employeesShiftDbContext.Employees.Add(employee);

        await _employeesShiftDbContext.SaveChangesAsync();
        return employee.Id;
    }

    public async Task DeleteEmployee(int id)
    {
        var employee = await _employeesShiftDbContext.Employees.FindAsync(id);
        if (employee == null)
            throw new KeyNotFoundException();

        _employeesShiftDbContext.Employees.Remove(employee);

        await _employeesShiftDbContext.SaveChangesAsync();
    }

    public Task UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = _mapper.Map<EmployeeEntity>(updateEmployeeDto);
        _employeesShiftDbContext.Employees.Update(employee);
        return _employeesShiftDbContext.SaveChangesAsync();
    }
}
