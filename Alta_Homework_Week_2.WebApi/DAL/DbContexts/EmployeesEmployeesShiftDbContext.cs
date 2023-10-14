using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.DAL.DbContexts
{
    public sealed class EmployeesEmployeesShiftDbContext : DbContext, IEmployeesShiftDbContext
    {
        public DbSet<JobTitleEntity> JobTitles { get; set; } = null!;
        public DbSet<EmployeeEntity> Employees { get; set; } = null!;
        public DbSet<ShiftRecordEntity> ShiftRecords { get; set; } = null!;

        public EmployeesEmployeesShiftDbContext(DbContextOptions<EmployeesEmployeesShiftDbContext> options) :
            base(options) => Database.EnsureCreated();
    }
}
