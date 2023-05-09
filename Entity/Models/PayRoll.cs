using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models;

public class PayRoll : BaseEntity
{
    [Key]
    public int PayrollId { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public DateTime PayDate { get; set; }
    public decimal GrossPay { get; set; }
    public decimal Taxes { get; set; }
    public decimal NetPay { get; set; }
    public virtual Employee Employee { get; set; }
}
