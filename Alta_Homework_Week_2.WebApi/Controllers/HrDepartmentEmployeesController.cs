using Alta_Homework_Week_2.WebApi.DTOs;
using Alta_Homework_Week_2.WebApi.Exceptions;
using Alta_Homework_Week_2.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Alta_Homework_Week_2.WebApi.Controllers
{
    public class HrDepartmentEmployeesController : BaseController
    {
        private readonly ILogger<HrDepartmentEmployeesController> _logger;
        private readonly IEmployeesRepository _employeesRepository;

        public HrDepartmentEmployeesController(ILogger<HrDepartmentEmployeesController> logger,
            IEmployeesRepository employeesRepository)
        {
            _logger = logger;
            _employeesRepository = employeesRepository;
        }

        /// <summary>
        /// Возвращает список всех сотрудников 
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<EmployeeVm>>> EmployeesAsync()
        {
            var employees = await _employeesRepository.GetEmployees();

            _logger.LogInformation("Запрошены данные всех сотрудников. ip адрес запросившего {ip}",
                HttpContext.Connection.RemoteIpAddress);

            return Ok(employees);
        }

        /// <summary>
        /// Возвращает информацию о сотруднике
        /// </summary>
        /// <param name="id">Id пропуска сотрудника</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeVm>> EmployeesAsync(int id)
        {
            try
            {
                var employee = await _employeesRepository.GetEmployee(id);

                _logger.LogInformation("Запрошены данные сотрудника с id {id}. ip адрес запросившего {ip}",
                    id, HttpContext.Connection.RemoteIpAddress);

                return Ok(employee);
            }
            catch (Exception e)
            {
                if (e is EmployeeNotFoundException)
                    return NotFound("Id не найден");
                throw;
            }
        }

        /// <summary>
        /// Добавляет данные о новом сотруднике
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddNewEmployeeAsync([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            try
            {
                await _employeesRepository.AddNewEmployee(createEmployeeDto);

                _logger.LogInformation("Добавлен новый сотрудник. ip адрес добавившего {ip}",
                    HttpContext.Connection.RemoteIpAddress);

                return Ok();
            }
            catch (Exception e)
            {
                if (e is DbUpdateException)
                    return BadRequest("Некорректные данные");
                throw;
            }
        }

        /// <summary>
        ///  обновляет данные о сотруднике
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            await _employeesRepository.UpdateEmployee(updateEmployeeDto);

            _logger.LogInformation("Данные сотрудника с id {id} обновлены. ip адрес обновившего {ip}",
                updateEmployeeDto.Id, HttpContext.Connection.RemoteIpAddress);

            return Ok();
        }

        /// <summary>
        /// Удаляет данные о сотруднике
        /// </summary>
        /// <param name="id">Id пропуска сотрудника</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int id)
        {
            try
            {
                await _employeesRepository.DeleteEmployee(id);

                _logger.LogInformation("Сотрудник с id {id} удалён. ip адрес удалившего {ip}",
                    id, HttpContext.Connection.RemoteIpAddress);

                return Ok();
            }
            catch (Exception e)
            {
                if (e is EmployeeNotFoundException)
                    return BadRequest("Такой id не существует");
                throw;
            }
        }
    }
}
