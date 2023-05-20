using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.UnitOfWorks;
using Service.Interfaces;
using Service.ViewModels.BenefitViewModels;
using Service.ViewModels.Common;
using Service.ViewModels.DepartmentViewModels;
using Service.ViewModels.EmployeeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations;

public class DepartmentService : IDepartmentService
{
    public readonly IUnitOfWork _unitOfWork;
    public ILogger<DepartmentService> _logger;
    public DepartmentService(IUnitOfWork unitOfWork, ILogger<DepartmentService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public bool Create(AddDepartmentViewModel model)
    {
        try
        {
            var isDepartmentNameMatched = _unitOfWork.Department.Any(d => d.DepartmentName == model.DepartmentName);
            if (!isDepartmentNameMatched)
            {
                if (model != null)
                {
                    var department = new Department();
                    department.DepartmentId = model.DepartmentId;
                    department.DepartmentName = model.DepartmentName;
                    department.ManagerId = model.ManagerId;
                    department.Description = model.Description;
                    department.CreationDate = DateTime.Now;
                    _unitOfWork.Department.Create(department);
                    _unitOfWork.SaveChangesAsync();
                }
                return true;
            }
            else
                return false;
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
            var isDependentToEmployee = _unitOfWork.Employee
               .Where(e => e.IsDeleted != true)
               .Any(x => x.DepartmentId == id);
            if (!isDependentToEmployee)
            {
                var department = _unitOfWork.Department.GetById(id);

                if (department != null)
                {
                    _unitOfWork.Department.Delete(department);
                    _unitOfWork.SaveChangesAsync();

                }
                return true;
            }
            else
                return false;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }

    public List<DepartmentListViewModel> GetAll()
    {
        try
        {
            var employee = _unitOfWork.Employee.ToList();
            var departments = _unitOfWork.Department.ToList();
            var department = _unitOfWork.Department
                .Where(i => i.IsDeleted != true)
                .AsEnumerable()
                .Select(x => new DepartmentListViewModel
            {
                DepartmentId = x.DepartmentId,
                DepartmentName = x.DepartmentName,
                ManagerId = x.ManagerId,
                ManagerFirstName = x.ManagerId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.ManagerId).FirstName) : "",
                ManagerLastName = x.ManagerId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.ManagerId).LastName) : "",
                Description = x.Description
            }).ToList();
            return department;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<DepartmentListViewModel>();
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

    public UpdateDepartmentViewModel GetById(int id)
    {
        try
        {
            var department = _unitOfWork.Department.FirstOrDefault(x => x.DepartmentId == id);
            var viewModel = new UpdateDepartmentViewModel();
            if (department != null)
            {
                viewModel.DepartmentId = department.DepartmentId;
                viewModel.DepartmentName = department.DepartmentName;
                viewModel.ManagerId = department.ManagerId;
                viewModel.Description = department.Description;
            }
            return viewModel;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new UpdateDepartmentViewModel();
        }
    }

    public bool SoftDelete(int id)
    {
        try
        {
            var isDependentToEmployee = _unitOfWork.Employee
                .Where(e => e.IsDeleted != true)
                .Any(x => x.DepartmentId == id);
            if (!isDependentToEmployee)
            {
                var department = _unitOfWork.Department.GetById(id);

                if (department != null)
                {
                    department.IsDeleted = true;
                    _unitOfWork.SaveChangesAsync();

                }
                return true;
            }
            else
                return false;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool Update(UpdateDepartmentViewModel model)
    {
        try
        {
            var department = _unitOfWork.Department.GetById(model.DepartmentId);

            if (department != null)
            {
                department.DepartmentName = model.DepartmentName;
                department.ManagerId = model.ManagerId;
                department.Description = model.Description;
                department.UpdatedDate = DateTime.Now;
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
