using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.PayRollViewModels;

public class AddPayRollViewModel
{
    public int PayrollId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime PayDate { get; set; }
    public decimal GrossPay { get; set; }
    public decimal Taxes { get; set; }
    public decimal NetPay { get; set; }
}
