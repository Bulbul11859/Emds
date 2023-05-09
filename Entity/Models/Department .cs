using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models;
public class Department : BaseEntity
{
    [Key]
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }

    [ForeignKey("Employee")]
    [Column("EmployeeId")]
    public int? ManagerId { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}
