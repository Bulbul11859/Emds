using Entity.Models;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Service.Interfaces;
using Service.ViewModels.Common;
using Service.ViewModels.PayRollViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.ViewModels.BenefitViewModels;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWorks;

namespace Service.Implementations;

public class PayRollService : IPayRollService
{
    public readonly IUnitOfWork _unitOfWork;
    public ILogger<PayRollService> _logger;
    public PayRollService(IUnitOfWork unitOfWork, ILogger<PayRollService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public bool Create(AddPayRollViewModel model)
    {
        try
        {
            var record = _unitOfWork.PayRoll.FirstOrDefault(x => x.EmployeeId == model.EmployeeId); 
            if(record !=null)
            {
                record.IsDeleted = false; 
                _unitOfWork.SaveChangesAsync();
            }

            else if (model != null && record == null)
            {
                var payroll = new PayRoll();
                payroll.PayrollId = model.PayrollId;
                payroll.EmployeeId = model.EmployeeId;
                payroll.PayDate = model.PayDate;
                payroll.GrossPay = model.GrossPay;
                payroll.Taxes = model.Taxes;
                payroll.NetPay = model.NetPay;
                payroll.CreationDate = DateTime.Now;
                _unitOfWork.PayRoll.Create(payroll);
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
            var payRoll = _unitOfWork.PayRoll.GetById(id);
            if (payRoll != null)
            {
                _unitOfWork.PayRoll.Delete(payRoll);
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

    public List<PayRollListViewModel> GetAll()
    {
        try
        {
            var payroll = _unitOfWork.PayRoll
                .Entity()
                .Include(i=>i.Employee)
                .Where(i => !i.IsDeleted && !i.Employee.IsDeleted)
                .AsEnumerable()
                .Select(x => new PayRollListViewModel
            {
                PayrollId = x.PayrollId,
                EmployeeFirstName =x.Employee.FirstName,
                EmployeeLastName = x.Employee.LastName,
                PayDate = x.PayDate,
                GrossPay = x.GrossPay,
                Taxes = x.Taxes,
                NetPay = x.NetPay

            }).ToList();

            return payroll;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new List<PayRollListViewModel>();
        }
    }

    public List<Dropdown> GetAllEmployeeName()
    {
        try
        {
            var employelist = _unitOfWork.Employee.Where(i => i.IsDeleted != true).Select(x => new Dropdown
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

    public UpdatePayRollViewModel GetById(int id)
    {
        try
        {
            var payroll = _unitOfWork.PayRoll.FirstOrDefault(x => x.PayrollId == id);
            var viewModel = new UpdatePayRollViewModel();

            if (payroll != null)
            {
                viewModel.PayrollId = payroll.PayrollId;
                viewModel.EmployeeId = payroll.EmployeeId;
                viewModel.PayDate = payroll.PayDate;
                viewModel.GrossPay = payroll.GrossPay;
                viewModel.Taxes = payroll.Taxes;
                viewModel.NetPay = payroll.NetPay;
            }
            return viewModel;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new UpdatePayRollViewModel();
        }
    }

    public bool SoftDelete(int id)
    {
        try
        {
            var payroll = _unitOfWork.PayRoll.GetById(id);

            if (payroll != null)
            {

                payroll.IsDeleted = true;
               
                _unitOfWork.SaveChangesAsync();

            }
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public bool Update(UpdatePayRollViewModel model)
    {
        try
        {
            var payroll = _unitOfWork.PayRoll.GetById(model.PayrollId);

            if (payroll != null)
            {

                payroll.PayrollId = model.PayrollId;
                payroll.EmployeeId = model.EmployeeId;
                payroll.PayDate = model.PayDate;
                payroll.GrossPay = model.GrossPay;
                payroll.Taxes = model.Taxes;
                payroll.NetPay = model.NetPay;
                payroll.UpdatedDate = DateTime.Now;
                _unitOfWork.SaveChangesAsync();

            }
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
