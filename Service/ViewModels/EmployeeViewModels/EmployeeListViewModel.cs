using Entity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.ViewModels.EmployeeViewModels;

public class EmployeeListViewModel
{
    public int EmployeeId { get; set; }
    public string DepartmentName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string JobTitle { get; set; }
    public DateTime EmploymentDate { get; set; }
    public decimal Salary { get; set; }
   
}
