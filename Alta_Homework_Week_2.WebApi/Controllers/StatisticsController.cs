using Alta_Homework_Week_2.WebApi.DTOs;
using Alta_Homework_Week_2.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alta_Homework_Week_2.WebApi.Controllers;

public class StatisticsController : BaseController
{
    private readonly IShiftsRepository _shiftsRepository;

    public StatisticsController(IShiftsRepository shiftsRepository) =>
        _shiftsRepository = shiftsRepository;

    /// <summary>
    /// Возвращает все записи смен
    /// </summary>
    /// <param name="employeeId">Id пропуска сотрудника</param>
    [HttpGet]
    public async Task<ActionResult<List<ShiftVm>>> GetShiftsAsync([FromQuery] int? employeeId)
    {
        return employeeId != null
            ? Ok(await _shiftsRepository.GetShiftsAsync(employeeId.Value))
            : Ok(await _shiftsRepository.GetShiftsAsync());
    }

    /// <summary>
    /// Возвращает текущие смены
    /// </summary>
    /// <param name="employeeId">Id пропуска сотрудника</param>
    [HttpGet]
    public async Task<ActionResult<List<ShiftVm>>> ShiftsCurrentAsync([FromQuery] int? employeeId)
    {
        return employeeId != null
            ? Ok(await _shiftsRepository.GetCurrentShiftsAsync(employeeId.Value))
            : Ok(await _shiftsRepository.GetCurrentShiftsAsync());
    }
}
