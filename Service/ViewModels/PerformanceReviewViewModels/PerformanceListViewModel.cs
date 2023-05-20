namespace Service.ViewModels.PerformanceReviewViewModels;
public class PerformanceListViewModel
{
    public int PerformanceReviewId { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public string EmployeeFullName
    {
        get
        {
            return $"{EmployeeFirstName} {EmployeeLastName}";
        }
    }
    public int ReviewerId { get; set; }
    public string ReviewrFirstName { get; set; }
    public string ReviewrLastName { get; set; }
    public string ReviewrFullName
    {
        get
        {
            return $"{ReviewrFirstName} {ReviewrLastName}";
        }
    }
    public int? OverallRating { get; set; }
    public string Comments { get; set; }
}
