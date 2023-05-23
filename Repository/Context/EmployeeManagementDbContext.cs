using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Context;

public class EmployeeManagementDbContext : DbContext
{
    public EmployeeManagementDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<PerformanceReview> PerformanceReviews { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<JobHistory> JobHistorys { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<PayRoll> Payrolls { get; set; }
}
