using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.BenefitViewModels;

public class AddBenefitViewModel
{
    public int BenefitsId { get; set; }
    public int? EmployeeId { get; set; }
    public string HealthInsuranceProvider { get; set; }
    public string HealthInsurancePolicyNumber { get; set; }
    public string RetirementPlan { get; set; }
    public int VacationDays { get; set; }
   
}
