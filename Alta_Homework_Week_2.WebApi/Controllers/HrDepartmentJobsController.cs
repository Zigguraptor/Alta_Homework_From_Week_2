using System.ComponentModel.DataAnnotations;
using Alta_Homework_Week_2.WebApi.Common.Exceptions;
using Alta_Homework_Week_2.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alta_Homework_Week_2.WebApi.Controllers;

public class HrDepartmentJobsController : BaseController
{
    private readonly IJobsRepository _jobsRepository;

    public HrDepartmentJobsController(IJobsRepository jobsRepository) =>
        _jobsRepository = jobsRepository;

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetJobTitlesAsync() =>
        Ok(await _jobsRepository.GetJobTitles());

    [HttpPost]
    public async Task<IActionResult> AddNewJobAsync(
        [FromQuery] [Required] [MinLength(1, ErrorMessage = "Должен содержать хотя бы один символ")]
        string jobTitle)
    {
        jobTitle = jobTitle.Trim();

        try
        {
            await _jobsRepository.AddNewJobTitle(jobTitle);
        }
        catch (Exception e)
        {
            if (e is RecordAlreadyExistsException)
                return BadRequest("Такая должность уже существует");
            throw;
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteJobTitleAsync([FromQuery] [Required] string jobTitle)
    {
        try
        {
            await _jobsRepository.DeleteJobTitleAsync(jobTitle);
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
