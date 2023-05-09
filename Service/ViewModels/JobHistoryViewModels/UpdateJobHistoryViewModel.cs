using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.JobHistoryViewModels;

public class UpdateJobHistoryViewModel
{
    public int JobHistoryId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string JobTitle { get; set; }
    public string Department { get; set; }
    public string CompanyName { get; set; }
    public string Responsibilities { get; set; }
}
