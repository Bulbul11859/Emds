namespace Service.ViewModels.PerformanceReviewViewModels;

public class UpdatePerformanceReviewViewModels
{
    public int PerformanceReviewId { get; set; }
    public int? EmployeeId { get; set; }
    public int ReviewerId { get; set; }
    public int? OverallRating { get; set; }
    public string Comments { get; set; }
}
