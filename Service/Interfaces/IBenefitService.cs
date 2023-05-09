using Service.ViewModels.BenefitViewModels;
using Service.ViewModels.Common;

namespace Service.Interfaces;
public interface IBenefitService
{
    public List<BenefitListViewModel> GetAll();
    public List<Dropdown> GetAllEmployeeName();
    public bool Create(AddBenefitViewModel model); 
    public UpdateBenefitViewModel GetById(int id);
    public bool Update(UpdateBenefitViewModel model);
    public bool Delete(int id);
    public bool SoftDelete(int id);
}
