using Alta_Homework_Week_2.WebApi;
using Alta_Homework_Week_2.WebApi.Common.Services;
using Alta_Homework_Week_2.WebApi.Middleware;
using Alta_Homework_Week_2.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMappingProfiles();
builder.Services.AddDbContexts(builder.Configuration);

builder.Services.AddSingleton<IDateTimeService, CommonDateTimeUtcService>();

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IJobsRepository, JobsRepository>();
builder.Services.AddScoped<IShiftsRepository, ShiftsRepository>();

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
