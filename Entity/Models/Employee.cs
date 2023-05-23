
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models;

public class Employee : BaseEntity
{
    [Key]
    public int EmployeeId { get; set; }

    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string JobTitle { get; set; }
    public DateTime EmploymentDate { get; set; }
    public decimal Salary { get; set; }
    public virtual PerformanceReview PerformanceReviewFor { get; set; }
    [InverseProperty("Reviewer")]
    public virtual PerformanceReview PerformanceReviewBy { get; set; }
    public virtual Department Department { get; set; }
    public virtual PayRoll PayRoll { get; set; }
    public virtual ICollection<Benefit> Benefit { get; set; }
    public virtual ICollection<JobHistory> JobHistories { get; set; }   
}
