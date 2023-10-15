using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.DAL.DbContexts
{
    public interface IEmployeesShiftDbContext
    {
        public DbSet<JobTitleEntity> JobTitles { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<ShiftRecordEntity> ShiftRecords { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public void Init();
    }
}
