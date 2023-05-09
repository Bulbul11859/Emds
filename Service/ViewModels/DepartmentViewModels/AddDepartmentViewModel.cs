using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.DepartmentViewModels;

public class AddDepartmentViewModel
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int? ManagerId { get; set; }
    public string Description { get; set; }
}
