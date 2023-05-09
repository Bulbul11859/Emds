using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models;

public class Benefit : BaseEntity
{
    [Key]
    public int BenefitsId { get; set; }

    [ForeignKey("Employee")]
    public int? EmployeeId { get; set; }
    public string HealthInsuranceProvider { get; set; }
    public string HealthInsurancePolicyNumber { get; set; }
    public string RetirementPlan { get; set; }
    public int VacationDays { get; set; }
    public virtual Employee Employee { get; set; }
}
