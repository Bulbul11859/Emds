using Service.ViewModels.Common;
using Service.ViewModels.EmployeeViewModels;

namespace Service.Interfaces;

public interface IEmployeeService
{
    public List<EmployeeListViewModel>GetAll();
    public List<DepartmentDropdown> GetAllDepartment();
    public bool Create(AddEmployeeViewModel model);
    public UpdateEmployeeViewModel GetById(int id);
    public bool Update(UpdateEmployeeViewModel model);
    public bool Delete(int id);
    public bool SoftDelete(int id);
}
