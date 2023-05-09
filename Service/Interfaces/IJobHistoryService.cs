using Service.ViewModels.BenefitViewModels;
using Service.ViewModels.Common;
using Service.ViewModels.JobHistoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces;

public interface IJobHistoryService
{
    public List<JobHistoryListViewModel> GetAll();
    public List<Dropdown> GetAllEmployeeName();
    public bool Create(AddJobHistoryViewModel model);
    public UpdateJobHistoryViewModel GetById(int id);
    public bool Update(UpdateJobHistoryViewModel model);
    public bool Delete(int id);
    public bool SoftDelete(int id);
}
