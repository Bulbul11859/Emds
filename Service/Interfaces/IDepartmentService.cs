using Service.ViewModels.Common;
using Service.ViewModels.DepartmentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces;

public interface IDepartmentService
{
    public List<DepartmentListViewModel>GetAll();
    public List<Dropdown> GetAllEmployeeName();
    public bool Create(AddDepartmentViewModel model);
    public UpdateDepartmentViewModel GetById(int id);
    public bool Update(UpdateDepartmentViewModel model);
    public bool Delete(int id);
    public bool SoftDelete(int id);
}
