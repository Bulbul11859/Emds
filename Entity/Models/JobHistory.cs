using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models;

public class JobHistory : BaseEntity
{
    [Key]
    public int JobHistoryId { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string JobTitle { get; set; }
    public string Department { get; set; }
    public string CompanyName { get; set; }
    public string Responsibilities { get; set; }
    public virtual Employee Employee { get; set; }

}
