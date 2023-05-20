using Entity.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Repositories;

namespace Repository.UnitOfWorks;

public interface IUnitOfWork 
{
    IBaseRepository<Employee> Employee { get; }
    IBaseRepository<Benefit> Benefit { get; }
    IBaseRepository<PerformanceReview> PerformanceReview { get; }
    IBaseRepository<Department> Department { get; }
    IBaseRepository<JobHistory> JobHistory { get; }
    IBaseRepository<PayRoll> PayRoll { get; }
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTranscationAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
