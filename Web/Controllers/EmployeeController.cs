using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels.EmployeeViewModels;
namespace Web.Controllers;

public class EmployeeController : Controller
{
    public readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var employee = _employeeService.GetAll();
        return View(employee);
    }
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.DepartmentList = _employeeService.GetAllDepartment();
        return View();
    }
    [HttpPost]
    public IActionResult Add(AddEmployeeViewModel addEmployeeViewModel)
    {
        bool isAdded = _employeeService.Create(addEmployeeViewModel);
        if (isAdded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Add");
        }
    }
    [HttpGet]
    public IActionResult View(int id)
    {
        var employeeViewModel = _employeeService.GetById(id);
        ViewBag.DepartmentList = _employeeService.GetAllDepartment();
        return View(employeeViewModel);
    }
    [HttpPost]
    public IActionResult Update(UpdateEmployeeViewModel model)
    {
        bool isUpdate = _employeeService.Update(model); 
        if (isUpdate)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("View", new {id=model.EmployeeId});
        }
    }
    public IActionResult Delete(int id)
    {
        bool isDeleted = _employeeService.Delete(id);
        if (isDeleted)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Index");
        }
    }
    public IActionResult SoftDelete(int id)
    {
        bool isSoftDeleted = _employeeService.SoftDelete(id);
        if (isSoftDeleted)
        {
            return RedirectToAction("Index");
        }
        else
        {
            TempData["MESSAGE"] = "Failed to delete this Employee because He/She is a Manager of Employee Table";
            return RedirectToAction("Index");
        }
    }
}
