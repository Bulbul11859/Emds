using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models;

public class PerformanceReview : BaseEntity
{

    [Key]
    public int PerformanceReviewId { get; set; }
    public int? EmployeeId { get; set; }
    public int ReviewerId { get; set; }
    public int? OverallRating { get; set; }
    public string Comments { get; set; }
    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; }
    [ForeignKey("ReviewerId")]
    public virtual Employee Reviewer { get; set; }
}
