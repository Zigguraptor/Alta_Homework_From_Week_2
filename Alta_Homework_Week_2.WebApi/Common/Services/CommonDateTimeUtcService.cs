namespace Alta_Homework_Week_2.WebApi.Common.Services;

public class CommonDateTimeUtcService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}
