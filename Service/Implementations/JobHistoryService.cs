using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Service.Interfaces;
using Service.ViewModels.BenefitViewModels;
using Service.ViewModels.Common;
using Service.ViewModels.EmployeeViewModels;
using Service.ViewModels.JobHistoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations;

public class JobHistoryService : IJobHistoryService
{
    public readonly EmployeeManagementDbContext _db;
    public ILogger<JobHistoryService> _logger;
    public JobHistoryService(EmployeeManagementDbContext db, ILogger<JobHistoryService> logger)
    {
        _db = db;
        _logger = logger;
    }
    public bool Create(AddJobHistoryViewModel model)
    {
        try
        {
            if(model != null)
            {
                var jobHistory=new JobHistory();
                jobHistory.JobHistoryId = model.JobHistoryId;
                jobHistory.EmployeeId=model.EmployeeId; 
                  jobHistory.StartDate=model.StartDate;   
                jobHistory.EndDate=model.EndDate;   
                jobHistory.JobTitle=model.JobTitle;
                jobHistory.Department=model.Department;
                jobHistory.CompanyName=model.CompanyName;   
                jobHistory.Responsibilities=model.Responsibilities;
                jobHistory.CreationDate = DateTime.Now;
                _db.JobHistorys.Add(jobHistory);    
                _db.SaveChanges();
            }
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var jobHistory = _db.JobHistorys.Find(id);
            if (jobHistory != null)
            {
                _db.JobHistorys.Remove(jobHistory);
                _db.SaveChanges();
            }
            return true;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }

    public List<JobHistoryListViewModel> GetAll()
    {
        try
        {
            var employee = _db.Employees.ToList();
            var jobHistory = _db.JobHistorys
                .Include(i=>i.Employee)
                .Where(i => !i.IsDeleted && !i.Employee.IsDeleted)
                .AsEnumerable()
                .Select(x => new JobHistoryListViewModel
            {
                JobHistoryId = x.JobHistoryId,
                EmployeeId = x.EmployeeId,
                EmployeeFirstName = x.Employee.FirstName ?? string.Empty,
                EmployeeLastName = x.Employee.LastName ?? string.Empty,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                JobTitle = x.JobTitle,
                Department = x.Department,
                CompanyName = x.CompanyName,    
                Responsibilities = x.Responsibilities
            }).ToList();
            return jobHistory;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new List<JobHistoryListViewModel>();
        }
    }

    public List<Dropdown> GetAllEmployeeName()
    {
        try
        {
            var employelist = _db.Employees.Where(i => i.IsDeleted != true).Select(x => new Dropdown
            {
                Id = x.EmployeeId,
                Name = x.FirstName + " " + x.LastName
            }).ToList();
            return employelist;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new List<Dropdown>();
        }
    }

    public UpdateJobHistoryViewModel GetById(int id)
    {
        try
        {
            var jobHistory=_db.JobHistorys.FirstOrDefault(x=>x.JobHistoryId==id);
            var viewModel = new UpdateJobHistoryViewModel();
            if(jobHistory != null)
            {
                viewModel.JobHistoryId = jobHistory.JobHistoryId;
                viewModel.EmployeeId=jobHistory.EmployeeId;
                viewModel.StartDate=jobHistory.StartDate;
                viewModel.EndDate=jobHistory.EndDate;
                viewModel.JobTitle = jobHistory.JobTitle;   
                viewModel.Department = jobHistory.Department;
                viewModel.CompanyName = jobHistory.CompanyName; 
                viewModel.Responsibilities=jobHistory.Responsibilities;
            }
            return viewModel;   
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new UpdateJobHistoryViewModel();
        }
    }

    public bool SoftDelete(int id)
    {
        try
        {
            var jobHistory = _db.JobHistorys.Find(id);
            if (jobHistory != null)
            {
                jobHistory.IsDeleted =true;
               
                _db.SaveChanges();

            }
            return true;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool Update(UpdateJobHistoryViewModel model)
    {
        try
        {
            var jobHistory = _db.JobHistorys.Find(model.JobHistoryId);
            if(jobHistory != null)
            {
                jobHistory.EmployeeId = model.EmployeeId;
                jobHistory.StartDate = model.StartDate;
                jobHistory.EndDate = model.EndDate;
                jobHistory.JobTitle = model.JobTitle;   
                jobHistory.Department = model.Department;
                jobHistory.CompanyName = model.CompanyName;
                jobHistory.Responsibilities = model.Responsibilities;
                jobHistory.UpdatedDate = DateTime.Now;
                _db.SaveChanges();

            }
            return true;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }
}
