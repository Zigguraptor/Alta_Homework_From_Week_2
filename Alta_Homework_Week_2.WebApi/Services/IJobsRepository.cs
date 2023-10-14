namespace Alta_Homework_Week_2.WebApi.Services;

public interface IJobsRepository
{
    public Task<List<string>> GetJobTitles();
    public Task AddNewJobTitle(string jobTitleEntity);
    public Task DeleteJobAsync(string jobTitle);
}
