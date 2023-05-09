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

namespace Service.Implementations;

public class PayRollService : IPayRollService
{
    public readonly EmployeeManagementDbContext _db;
    public ILogger<PayRollService> _logger;
    public PayRollService(EmployeeManagementDbContext db, ILogger<PayRollService> logger)
    {
        _db = db;
        _logger = logger;
    }
    public bool Create(AddPayRollViewModel model)
    {
        try
        {
            var record = _db.Payrolls.FirstOrDefault(x => x.EmployeeId == model.EmployeeId); 
            if(record !=null)
            {
                record.IsDeleted = false; 
                _db.SaveChanges();
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
                _db.Payrolls.Add(payroll);
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
            var payRoll = _db.Payrolls.Find(id);
            if (payRoll != null)
            {
                _db.Payrolls.Remove(payRoll);
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

    public List<PayRollListViewModel> GetAll()
    {
        try
        {
            var employee = _db.Employees.ToList();
            var payroll = _db.Payrolls
                .Include(i=>i.Employee)
                .Where(i => !i.IsDeleted && !i.Employee.IsDeleted)
                .AsEnumerable()
                .Select(x => new PayRollListViewModel
            {
                PayrollId = x.PayrollId,
                EmployeeFirstName = x.EmployeeId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.EmployeeId).FirstName) : "",
                EmployeeLastName = x.EmployeeId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.EmployeeId).LastName) : "",
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

    public UpdatePayRollViewModel GetById(int id)
    {
        try
        {
            var payroll = _db.Payrolls.FirstOrDefault(x => x.PayrollId == id);
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
            var payroll = _db.Payrolls.Find(id);

            if (payroll != null)
            {

                payroll.IsDeleted = true;
               
                _db.SaveChanges();

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
            var payroll = _db.Payrolls.Find(model.PayrollId);

            if (payroll != null)
            {

                payroll.PayrollId = model.PayrollId;
                payroll.EmployeeId = model.EmployeeId;
                payroll.PayDate = model.PayDate;
                payroll.GrossPay = model.GrossPay;
                payroll.Taxes = model.Taxes;
                payroll.NetPay = model.NetPay;
                payroll.UpdatedDate = DateTime.Now;
                _db.SaveChanges();

            }
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
