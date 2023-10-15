using Alta_Homework_Week_2.WebApi.Common.Exceptions;
using Alta_Homework_Week_2.WebApi.Common.Services;
using Alta_Homework_Week_2.WebApi.DAL.DbContexts;
using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Alta_Homework_Week_2.WebApi.DTOs;
using Alta_Homework_Week_2.WebApi.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.Services;

public class ShiftsRepository : IShiftsRepository
{
    private readonly IEmployeesShiftDbContext _employeesShiftDbContext;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;

    public ShiftsRepository(IEmployeesShiftDbContext employeesShiftDbContext, IDateTimeService dateTimeService,
        IMapper mapper)
    {
        _employeesShiftDbContext = employeesShiftDbContext;
        _dateTimeService = dateTimeService;
        _mapper = mapper;
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

    public Task<List<ShiftVm>> GetShiftsAsync()
    {
        return _employeesShiftDbContext.ShiftRecords
            .Include(entity => entity.EmployeeEntity)
            .Select(entity => _mapper.Map<ShiftVm>(entity))
            .ToListAsync();
    }

    public Task<List<ShiftVm>> GetShiftsAsync(int employeeId)
    {
        return _employeesShiftDbContext.ShiftRecords
            .Where(sr => sr.EmployeeId == employeeId)
            .Include(entity => entity.EmployeeEntity)
            .Select(entity => _mapper.Map<ShiftVm>(entity))
            .ToListAsync();
    }

    public Task<List<ShiftVm>> GetCurrentShiftsAsync()
    {
        return _employeesShiftDbContext.ShiftRecords
            .Where(sr => sr.EndTime == null)
            .Include(entity => entity.EmployeeEntity)
            .Select(entity => _mapper.Map<ShiftVm>(entity))
            .ToListAsync();
    }

    public Task<List<ShiftVm>> GetCurrentShiftsAsync(int employeeId)
    {
        return _employeesShiftDbContext.ShiftRecords
            .Where(sr => sr.EmployeeId == employeeId && sr.EndTime == null)
            .Include(entity => entity.EmployeeEntity)
            .Select(entity => _mapper.Map<ShiftVm>(entity))
            .ToListAsync();
    }
}
