using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.UnitOfWorks;
using Service.Interfaces;
using Service.ViewModels.BenefitViewModels;
using Service.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations;

public class BenefitService : IBenefitService
{
    public readonly IUnitOfWork _unitOfWork;
    public ILogger<BenefitService> _logger;
    public BenefitService(IUnitOfWork unitOfWork, ILogger<BenefitService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public bool Create(AddBenefitViewModel model)
    {
        try
        {
            if (model != null)
            {
                var benefit = new Benefit();

                benefit.BenefitsId = model.BenefitsId;
                benefit.EmployeeId = model.EmployeeId;
                benefit.HealthInsuranceProvider = model.HealthInsuranceProvider;
                benefit.HealthInsurancePolicyNumber = model.HealthInsurancePolicyNumber;
                benefit.RetirementPlan = model.RetirementPlan;
                benefit.VacationDays = model.VacationDays;
                benefit.CreationDate = DateTime.Now;
                _unitOfWork.Benefit.Create(benefit);
                _unitOfWork.SaveChangesAsync();
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
            var benefit = _unitOfWork.Benefit.GetById(id);
            if (benefit != null)
            {
                _unitOfWork.Benefit.Delete(benefit);
                _unitOfWork.SaveChangesAsync();
            }
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
    public List<BenefitListViewModel> GetAll()
    {
        try
        {
            var benefit = _unitOfWork.Benefit
                .Include(i => i.Employee)
                .Where(i => !i.IsDeleted && !i.Employee.IsDeleted)
                .AsEnumerable()
                .Select(x => new BenefitListViewModel
                {
                    BenefitsId = x.BenefitsId,
                    EmployeeFirstName = x.Employee.FirstName??string.Empty,
                    EmployeeLastName = x.Employee.LastName ?? string.Empty,
                    HealthInsuranceProvider = x.HealthInsuranceProvider,
                    HealthInsurancePolicyNumber = x.HealthInsurancePolicyNumber,
                    RetirementPlan = x.RetirementPlan,
                    VacationDays = x.VacationDays

                }).ToList();

            return benefit;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<BenefitListViewModel>();
        }
    }

    public List<Dropdown> GetAllEmployeeName()
    {
        try
        {
            var employelist = _unitOfWork.Employee.Where(i => i.IsDeleted != true).Select(x => new Dropdown
            {
                Id = x.EmployeeId,
                Name =$"{x.FirstName} {x.LastName}"
            }).ToList();
            return employelist;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new List<Dropdown>();
        }
    }

    public UpdateBenefitViewModel GetById(int id)
    {
        try
        {
            var benefit = _unitOfWork.Benefit.FirstOrDefault(x => x.BenefitsId == id);
            var viewModel = new UpdateBenefitViewModel();

            if (benefit != null)
            {
                viewModel.BenefitsId = benefit.BenefitsId;
                viewModel.EmployeeId = benefit.EmployeeId;
                viewModel.HealthInsuranceProvider = benefit.HealthInsuranceProvider;
                viewModel.HealthInsurancePolicyNumber = benefit.HealthInsurancePolicyNumber;
                viewModel.RetirementPlan = benefit.RetirementPlan;
                viewModel.VacationDays = benefit.VacationDays;
            }
            return viewModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new UpdateBenefitViewModel();
        }
    }

    public bool SoftDelete(int id)
    {
        try
        {
            var benefit = _unitOfWork.Benefit.GetById(id);

            if (benefit != null)
            {
                benefit.IsDeleted = true;

                _unitOfWork.SaveChangesAsync();

            }
            return true;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool Update(UpdateBenefitViewModel model)
    {
        try
        {
            var benefit = _unitOfWork.Benefit.GetById(model.BenefitsId);

            if (benefit != null)
            {
                benefit.EmployeeId = model.EmployeeId;
                benefit.HealthInsuranceProvider = model.HealthInsuranceProvider;
                benefit.HealthInsurancePolicyNumber = model.HealthInsurancePolicyNumber;
                benefit.RetirementPlan = model.RetirementPlan;
                benefit.VacationDays = model.VacationDays;
                benefit.UpdatedDate = DateTime.Now;
                _unitOfWork.SaveChangesAsync();

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



