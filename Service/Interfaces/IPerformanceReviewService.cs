using Service.ViewModels.BenefitViewModels;
using Service.ViewModels.Common;
using Service.ViewModels.PerformanceReviewViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces;

public interface IPerformanceReviewService
{
    public List<PerformanceListViewModel> GetAll();
    public List<Dropdown> GetAllEmployeeName();
    public bool Create(AddPerformanceReviewViewModels model);
    public UpdatePerformanceReviewViewModels GetById(int id);
    public bool Update(UpdatePerformanceReviewViewModels model);
    public bool Delete(int id);
    public bool SoftDelete(int id);
}
