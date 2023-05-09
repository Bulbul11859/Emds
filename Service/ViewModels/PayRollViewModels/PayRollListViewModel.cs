using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.PayRollViewModels;

public class PayRollListViewModel
{
    public int PayrollId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public string EmployeeFullName
    {
        get
        {
            return $"{EmployeeFirstName} {EmployeeLastName}";
        }
    }
    public DateTime PayDate { get; set; }
    public decimal GrossPay { get; set; }
    public decimal Taxes { get; set; }
    public decimal NetPay { get; set; }
}
