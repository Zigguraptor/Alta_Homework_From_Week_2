using Alta_Homework_Week_2.WebApi.Common.Exceptions;
using Alta_Homework_Week_2.WebApi.Exceptions;
using Alta_Homework_Week_2.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alta_Homework_Week_2.WebApi.Controllers
{
    public class CheckpointController : BaseController
    {
        private readonly ILogger<CheckpointController> _logger;
        private readonly IShiftsRepository _shiftsRepository;

        public CheckpointController(ILogger<CheckpointController> logger, IShiftsRepository shiftsRepository)
        {
            _logger = logger;
            _shiftsRepository = shiftsRepository;
        }

        /// <summary>
        /// Начинает смену сотрудника по id его пропуска
        /// </summary>
        /// <param name="employeeId">Id пропуска сотрудника</param>
        [HttpPost]
        public async Task<IActionResult> StartShiftAsync([FromQuery] int employeeId)
        {
            try
            {
                await _shiftsRepository.StartShift(employeeId);

                _logger.LogInformation("Сотрудник с id: {id} пришёл на смену", employeeId);
                return Ok();
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case EmployeeNotFoundException:
                        return BadRequest("Сотрудник с таким id не найден");
                    case RecordAlreadyExistsException:
                        return BadRequest("Сотрудник уже на смене");
                    default:
                        _logger.LogError("Неожиданное исключение при попытке начать смену. employeeId: {id}. {e}",
                            employeeId, e);
                        throw;
                }
            }
        }

        /// <summary>
        /// Завершает смену сотрудника по id его пропуска
        /// </summary>
        /// <param name="employeeId">Id пропуска сотрудника</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EndShiftAsync([FromQuery] int employeeId)
        {
            try
            {
                await _shiftsRepository.EndShift(employeeId);

                _logger.LogInformation("Сотрудник с id: {id} закончил смену", employeeId);
                return Ok();
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case EmployeeNotFoundException:
                        return BadRequest("Сотрудник с таким id не найден");
                    case RecordNotFoundException:
                        return BadRequest("Сотрудник не находится на смене в данный момент");
                    default:
                        _logger.LogError("Неожиданное исключение при попытке завершить смену. employeeId: {id}. {e}",
                            employeeId, e);
                        throw;
                }
            }
        }
    }
}
