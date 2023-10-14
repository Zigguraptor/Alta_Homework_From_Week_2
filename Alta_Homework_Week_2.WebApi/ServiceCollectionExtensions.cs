using Alta_Homework_Week_2.WebApi.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContexts(this IServiceCollection serviceCollection,
        ConfigurationManager configurationManager)
    {
        serviceCollection.AddDbContext<IEmployeesShiftDbContext, EmployeesEmployeesShiftDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlite(configurationManager.GetConnectionString("EmployeesShiftDb"));
        });

        return serviceCollection;
    }
}
