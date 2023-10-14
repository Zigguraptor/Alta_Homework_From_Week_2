﻿using Alta_Homework_Week_2.WebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alta_Homework_Week_2.WebApi.DAL.DbContexts
{
    public interface IEmployeesShiftDbContext
    {
        DbSet<JobTitleEntity> JobTitles { get; set; }
        DbSet<EmployeeEntity> Employees { get; set; }
        DbSet<ShiftRecordEntity> ShiftRecords { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}