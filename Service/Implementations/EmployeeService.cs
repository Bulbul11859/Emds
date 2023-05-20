using Entity.Models;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.UnitOfWorks;
using Service.Interfaces;
using Service.ViewModels.Common;
using Service.ViewModels.EmployeeViewModels;

namespace Service.Implementations;
public class EmployeeService : IEmployeeService
{
    public readonly IUnitOfWork _unitOfWork;
    public ILogger<EmployeeService> _logger;

    public EmployeeService(IUnitOfWork unitOfWork, ILogger<EmployeeService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public bool Create(AddEmployeeViewModel model)
    {
        try
        {
            var employee = new Employee();

            employee.EmployeeId = model.EmployeeId;
            employee.DepartmentId = model.DepartmentId;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.PhoneNumber = model.PhoneNumber;
            employee.DateOfBirth = model.DateOfBirth;
            employee.Address = model.Address;
            employee.JobTitle = model.JobTitle;
            employee.EmploymentDate = model.EmploymentDate;
            employee.Salary = model.Salary;
            employee.CreationDate = DateTime.Now;

            _unitOfWork.Employee.Create(employee);
            _unitOfWork.SaveChangesAsync();  
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
            var isDependentToDepartment = _unitOfWork.Department.
                 Where(d => d.IsDeleted != true)
                 .Any(x => x.ManagerId == id);
            if (!isDependentToDepartment)
            {
                var employee = _unitOfWork.Employee.GetById(id);
                if (employee != null)
                {
                    _unitOfWork.Employee.Delete(employee);
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

    public List<EmployeeListViewModel> GetAll()
    {
        try
        {
            var departman = _unitOfWork.Department.GetAllEnumerable();
            var employee = _unitOfWork.Employee
                .Where(i => i.IsDeleted != true)
                .AsEnumerable()
                .Select(x => new EmployeeListViewModel
                {
                    EmployeeId = x.EmployeeId,
                    DepartmentName = x.DepartmentId != null ? (departman.FirstOrDefault(e => e.DepartmentId == x.DepartmentId).DepartmentName) : "",
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    DateOfBirth = x.DateOfBirth,
                    Address = x.Address,
                    JobTitle = x.JobTitle,
                    EmploymentDate = x.EmploymentDate,
                    Salary = x.Salary
                }).ToList();
            return employee;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new List<EmployeeListViewModel>();
        }

    }

    public List<DepartmentDropdown> GetAllDepartment()
    {
        try
        {
            var department = _unitOfWork.Department.Where(i => !i.IsDeleted).Select(x => new DepartmentDropdown
            {
                Id = x.DepartmentId,
                DepartmentName = x.DepartmentName
            }).ToList();
            return department;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public UpdateEmployeeViewModel GetById(int id)
    {
        try
        {
            var employee = _unitOfWork.Employee.FirstOrDefault(x => x.EmployeeId == id);
            var viewModel = new UpdateEmployeeViewModel();

            if (employee != null)
            {
                viewModel.EmployeeId = employee.EmployeeId;
                viewModel.DepartmentId = employee.DepartmentId;
                viewModel.FirstName = employee.FirstName;
                viewModel.LastName = employee.LastName;
                viewModel.Email = employee.Email;
                viewModel.PhoneNumber = employee.PhoneNumber;
                viewModel.DateOfBirth = employee.DateOfBirth;
                viewModel.Address = employee.Address;
                viewModel.JobTitle = employee.JobTitle;
                viewModel.EmploymentDate = employee.EmploymentDate;
                viewModel.Salary = employee.Salary;
            }
            return viewModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new UpdateEmployeeViewModel();
        }
    }

    public bool SoftDelete(int id)
    {
        try
        {
            var isDependentToDepartment = _unitOfWork.Department.
                Where(d => d.IsDeleted != true)
                .Any(x => x.ManagerId == id);
            if (!isDependentToDepartment)
            {
                var employee = _unitOfWork.Employee.GetById(id);
                if (employee != null)
                {
                    employee.IsDeleted = true;
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

    public bool Update(UpdateEmployeeViewModel model)
    {
        try
        {
            var employee = _unitOfWork.Employee.GetById(model.EmployeeId);
            if (employee != null)
            {
                employee.DepartmentId = model.DepartmentId;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Address = model.Address;
                employee.JobTitle = model.JobTitle;
                employee.EmploymentDate = model.EmploymentDate;
                employee.Salary = model.Salary;
                employee.UpdatedDate = DateTime.Now;

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
