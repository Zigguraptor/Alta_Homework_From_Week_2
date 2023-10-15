using Alta_Homework_Week_2.WebApi;
using Alta_Homework_Week_2.WebApi.Common.Services;
using Alta_Homework_Week_2.WebApi.Middleware;
using Alta_Homework_Week_2.WebApi.Services;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

// logging
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

// DBs and mapping
    builder.Services.AddAutoMappingProfiles();
    builder.Services.AddDbContexts(builder.Configuration);

// custom services
    builder.Services.AddSingleton<IDateTimeService, CommonDateTimeUtcService>();

    builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
    builder.Services.AddScoped<IJobsRepository, JobsRepository>();
    builder.Services.AddScoped<IShiftsRepository, ShiftsRepository>();

// other
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlerMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
