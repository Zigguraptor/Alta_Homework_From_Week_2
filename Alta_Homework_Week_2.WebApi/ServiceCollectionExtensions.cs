using System.Reflection;
using Alta_Homework_Week_2.WebApi.Common.Mappings;
using Alta_Homework_Week_2.WebApi.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMappingProfiles(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });

        return serviceCollection;
    }

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
