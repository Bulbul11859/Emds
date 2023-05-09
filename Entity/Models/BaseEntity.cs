namespace Entity.Models;
public class BaseEntity
{
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? CreationDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;

}
