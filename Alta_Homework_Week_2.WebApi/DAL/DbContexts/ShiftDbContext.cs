using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.DAL.DbContexts
{
    public sealed class ShiftDbContext : DbContext, IShiftDbContext
    {
        public DbSet<JobTitle> JobTitles { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<ShiftRecord> ShiftRecords { get; set; } = null!;

        public ShiftDbContext(DbContextOptions<ShiftDbContext> options) : base(options) => Database.EnsureCreated();
    }
}
