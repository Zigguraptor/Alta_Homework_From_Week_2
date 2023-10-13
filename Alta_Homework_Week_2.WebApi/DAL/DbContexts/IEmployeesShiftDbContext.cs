using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.DAL.DbContexts
{
    public interface IEmployeesShiftDbContext
    {
        DbSet<JobTitle> JobTitles { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<ShiftRecord> ShiftRecords { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
