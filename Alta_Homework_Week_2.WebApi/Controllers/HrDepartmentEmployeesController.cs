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
        private readonly IEmployeesRepository _employeesRepository;

        public HrDepartmentEmployeesController(IEmployeesRepository employeesRepository) =>
            _employeesRepository = employeesRepository;

        #region Employees

        [HttpGet]
        public async Task<ActionResult<List<EmployeeVm>>> EmployeesAsync() =>
            Ok(await _employeesRepository.GetEmployees());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeVm>> EmployeesAsync(int id)
        {
            try
            {
                return Ok(await _employeesRepository.GetEmployee(id));
            }
            catch (Exception e)
            {
                if (e is EmployeeNotFoundException)
                    return NotFound("Id не найден");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployeeAsync([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            try
            {
                await _employeesRepository.AddNewEmployee(createEmployeeDto);
            }
            catch (Exception e)
            {
                if (e is DbUpdateException)
                    return BadRequest("Некорректные данные");
                throw;
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            await _employeesRepository.UpdateEmployee(updateEmployeeDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int id)
        {
            try
            {
                await _employeesRepository.DeleteEmployee(id);
            }
            catch (Exception e)
            {
                if (e is EmployeeNotFoundException)
                    return BadRequest("Такой id не существует");
                throw;
            }

            return Ok();
        }

        #endregion
    }
}
