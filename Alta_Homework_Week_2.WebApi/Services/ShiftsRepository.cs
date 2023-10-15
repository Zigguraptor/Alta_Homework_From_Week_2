using Alta_Homework_Week_2.WebApi.Common.Exceptions;
using Alta_Homework_Week_2.WebApi.Common.Services;
using Alta_Homework_Week_2.WebApi.DAL.DbContexts;
using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Alta_Homework_Week_2.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.Services;

public class ShiftsRepository : IShiftsRepository
{
    private readonly IEmployeesShiftDbContext _employeesShiftDbContext;
    private readonly IDateTimeService _dateTimeService;

    public ShiftsRepository(IEmployeesShiftDbContext employeesShiftDbContext, IDateTimeService dateTimeService)
    {
        _employeesShiftDbContext = employeesShiftDbContext;
        _dateTimeService = dateTimeService;
    }

    public async Task StartShift(int employeeId)
    {
        if (!_employeesShiftDbContext.Employees.Any(e => e.Id == employeeId))
            throw new EmployeeNotFoundException();

        var shiftRecord = _employeesShiftDbContext.ShiftRecords
            .Where(sr => sr.EmployeeId == employeeId)
            .OrderByDescending(sr => sr.StartTime)
            .FirstOrDefault();

        if (shiftRecord is { EndTime: null })
            throw new RecordAlreadyExistsException();

        var shiftRecordEntity = new ShiftRecordEntity
        {
            StartTime = _dateTimeService.Now,
            EndTime = null,
            EmployeeId = employeeId
        };

        _employeesShiftDbContext.ShiftRecords.Add(shiftRecordEntity);
        await _employeesShiftDbContext.SaveChangesAsync();
    }

    public Task EndShift(int employeeId)
    {
        if (!_employeesShiftDbContext.Employees.Any(e => e.Id == employeeId))
            throw new EmployeeNotFoundException();

        var shiftRecord = _employeesShiftDbContext.ShiftRecords
            .FirstOrDefault(shift => shift.EmployeeId == employeeId && shift.EndTime == null);

        if (shiftRecord == null)
            throw new RecordNotFoundException();

        shiftRecord.EndTime = _dateTimeService.Now;

        return _employeesShiftDbContext.SaveChangesAsync();
    }

    public Task<List<ShiftRecordEntity>> GetShiftsAsync()
    {
        return _employeesShiftDbContext.ShiftRecords.ToListAsync();
    }

    public Task<List<ShiftRecordEntity>> GetShiftsAsync(int employeeId)
    {
        return _employeesShiftDbContext.ShiftRecords
            .Where(sr => sr.EmployeeId == employeeId)
            .ToListAsync();
    }

    public Task<List<ShiftRecordEntity>> GetCurrentShiftsAsync()
    {
        return _employeesShiftDbContext.ShiftRecords
            .Where(sr => sr.EndTime == null)
            .ToListAsync();
    }

    public Task<List<ShiftRecordEntity>> GetCurrentShiftsAsync(int employeeId)
    {
        return _employeesShiftDbContext.ShiftRecords
            .Where(sr => sr.EmployeeId == employeeId && sr.EndTime == null)
            .ToListAsync();
    }
}
