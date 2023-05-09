namespace Service.ViewModels.BenefitViewModels;

public class BenefitListViewModel
{
    public int BenefitsId { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public string EmployeeFullName {
        get 
        {
            return $"{EmployeeFirstName} {EmployeeLastName}";
        } 
    }
    public string HealthInsuranceProvider { get; set; }
    public string HealthInsurancePolicyNumber { get; set; }
    public string RetirementPlan { get; set; }
    public int VacationDays { get; set; }
}
