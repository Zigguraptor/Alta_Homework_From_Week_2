using Alta_Homework_Week_2.WebApi.Common.Exceptions;
using Alta_Homework_Week_2.WebApi.DAL.DbContexts;
using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.Services;

public class JobsRepository : IJobsRepository
{
    private readonly IEmployeesShiftDbContext _employeesShiftDbContext;

    public JobsRepository(IEmployeesShiftDbContext employeesShiftDbContext) =>
        _employeesShiftDbContext = employeesShiftDbContext;

    public Task<List<string>> GetJobTitles() =>
        _employeesShiftDbContext.JobTitles.Select(j => j.Title).ToListAsync();

    public async Task AddNewJobTitle(string jobTitle)
    {
        if (await _employeesShiftDbContext.JobTitles.FindAsync(jobTitle) != null)
            throw new RecordAlreadyExistsException();

        _employeesShiftDbContext.JobTitles.Add(new JobTitleEntity { Title = jobTitle });

        await _employeesShiftDbContext.SaveChangesAsync();
    }

    public async Task DeleteJobTitleAsync(string jobTitle)
    {
        var localJobTitle = await _employeesShiftDbContext.JobTitles.FindAsync(jobTitle);
        if (localJobTitle == null)
            throw new RecordNotFoundException();

        _employeesShiftDbContext.JobTitles.Remove(localJobTitle);
        await _employeesShiftDbContext.SaveChangesAsync();
    }
}
