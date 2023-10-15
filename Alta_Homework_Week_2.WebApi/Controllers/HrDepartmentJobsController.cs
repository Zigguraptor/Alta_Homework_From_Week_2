using System.ComponentModel.DataAnnotations;
using Alta_Homework_Week_2.WebApi.Common.Exceptions;
using Alta_Homework_Week_2.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alta_Homework_Week_2.WebApi.Controllers;

public class HrDepartmentJobsController : BaseController
{
    private readonly ILogger<HrDepartmentJobsController> _logger;
    private readonly IJobsRepository _jobsRepository;

    public HrDepartmentJobsController(ILogger<HrDepartmentJobsController> logger, IJobsRepository jobsRepository)
    {
        _logger = logger;
        _jobsRepository = jobsRepository;
    }

    /// <summary>
    /// Возвращает список всех должностей
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<string>>> GetJobTitlesAsync() =>
        Ok(await _jobsRepository.GetJobTitles());

    /// <summary>
    /// Создаёт новую должность
    /// </summary>
    /// <param name="jobTitle">Должность</param>
    [HttpPost]
    public async Task<IActionResult> AddNewJobAsync(
        [FromQuery] [Required] [MinLength(1, ErrorMessage = "Должен содержать хотя бы один символ")]
        string jobTitle)
    {
        jobTitle = jobTitle.Trim();

        try
        {
            await _jobsRepository.AddNewJobTitle(jobTitle);

            _logger.LogInformation("Создана новая должность {jobTitle}. ip адрес создавшего {ip}",
                jobTitle, HttpContext.Connection.RemoteIpAddress);
        }
        catch (Exception e)
        {
            if (e is RecordAlreadyExistsException)
                return BadRequest("Такая должность уже существует");
            throw;
        }

        return Ok();
    }

    /// <summary>
    /// Удаляет существующую должность
    /// </summary>
    /// <param name="jobTitle">Должность</param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteJobTitleAsync([FromQuery] [Required] string jobTitle)
    {
        try
        {
            await _jobsRepository.DeleteJobTitleAsync(jobTitle);

            _logger.LogInformation("Удалена должность {jobTitle}. ip адрес удалившего {ip}",
                jobTitle, HttpContext.Connection.RemoteIpAddress);
        }
        catch (Exception e)
        {
            if (e is RecordNotFoundException)
                return BadRequest("Такой должности не существует");
            throw;
        }

        return Ok();
    }
}
