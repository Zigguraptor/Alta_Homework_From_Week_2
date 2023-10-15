using Alta_Homework_Week_2.WebApi.Common.Exceptions;
using Alta_Homework_Week_2.WebApi.Exceptions;
using Alta_Homework_Week_2.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alta_Homework_Week_2.WebApi.Controllers
{
    public class CheckpointController : BaseController
    {
        private readonly IShiftsRepository _shiftsRepository;

        public CheckpointController(IShiftsRepository shiftsRepository) =>
            _shiftsRepository = shiftsRepository;

        [HttpPost]
        public async Task<IActionResult> StartShiftAsync([FromQuery] int employeeId)
        {
            try
            {
                await _shiftsRepository.StartShift(employeeId);

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
                        throw;
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> EndShiftAsync([FromQuery] int employeeId)
        {
            try
            {
                await _shiftsRepository.EndShift(employeeId);

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
                        throw;
                }
            }
        }
    }
}
