using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.DepartmentViewModels;

public class DepartmentListViewModel
{

    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int? ManagerId { get; set; }
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
    public string ManagerFullName
    {
        get
        {
            return $"{ManagerFirstName} {ManagerLastName}";
        }
    }
    public string Description { get; set; }
}
