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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Employee entity configuration
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId); // Define the primary key
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            // Other properties and configurations...

            // Define the relationships using Fluent API
            entity.HasOne(e => e.Department) // Employee has one Department
                  .WithMany(d => d.Employees) // Department has many Employees
                  .HasForeignKey(e => e.DepartmentId) // Foreign key property
                  .OnDelete(DeleteBehavior.Restrict); // Define the delete behavior if needed

            entity.HasOne(e => e.PerformanceReviewFor) // Employee has one PerformanceReview (as a target)
                  .WithOne(p => p.Employee) // PerformanceReview has one Employee (as a source)
                  .HasForeignKey<PerformanceReview>(p => p.EmployeeId) // Foreign key property on PerformanceReview
                  .OnDelete(DeleteBehavior.Restrict); // Define the delete behavior if needed

            entity.HasOne(e => e.PerformanceReviewBy) // Employee has one PerformanceReview (as a reviewer)
                  .WithOne(p => p.Reviewer) // PerformanceReview has one Employee (as a reviewer)
                  .HasForeignKey<PerformanceReview>(p => p.ReviewerId) // Foreign key property on PerformanceReview
                  .OnDelete(DeleteBehavior.Restrict); // Define the delete behavior if needed

            // Add other relationships as needed...
        });

        // Define configurations for other entities if applicable...

        base.OnModelCreating(modelBuilder);
    }
}
