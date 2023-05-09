using Service.ViewModels.Common;
using Service.ViewModels.PayRollViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces;

public interface IPayRollService
{
    public List<PayRollListViewModel> GetAll();
    public List<Dropdown>GetAllEmployeeName();
    public bool Create(AddPayRollViewModel model);
    public UpdatePayRollViewModel GetById(int id);  
    public bool Update(UpdatePayRollViewModel model);
    public bool Delete(int id);
    public bool SoftDelete(int id);
}
