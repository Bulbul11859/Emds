using Entity.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories;

namespace Repository.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmployeeManagementDbContext _db;
    public IBaseRepository<Employee> Employee { get;}
    public IBaseRepository<Benefit> Benefit { get;}
    public IBaseRepository<PerformanceReview> PerformanceReview { get;}
    public IBaseRepository<Department> Department { get;}
    public IBaseRepository<JobHistory> JobHistory { get;}
    public IBaseRepository<PayRoll> PayRoll { get;}

    public UnitOfWork(EmployeeManagementDbContext db,
        IBaseRepository<Employee> EmployeeRepo,
        IBaseRepository<Benefit> BenefitRepo,
        IBaseRepository<PerformanceReview> PerformanceReviewRepo,
        IBaseRepository<Department> DepartmentRepo,
        IBaseRepository<JobHistory> JobHistoryRepo,
        IBaseRepository<PayRoll> PayRollRepo)
    {
        _db = db;
        Employee = EmployeeRepo;
        Benefit = BenefitRepo;
        PerformanceReview = PerformanceReviewRepo;
        Department = DepartmentRepo;
        JobHistory = JobHistoryRepo;
        PayRoll = PayRollRepo;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
    public async Task<IDbContextTransaction> BeginTranscationAsync()
    {
        return await _db.Database.BeginTransactionAsync();
    }
    public async Task CommitTransactionAsync()
    {
        await _db.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _db.Database.RollbackTransactionAsync();
    }
}
