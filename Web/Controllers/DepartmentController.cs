using Microsoft.AspNetCore.Mvc;
using Service.Implementations;
using Service.Interfaces;
using Service.ViewModels.DepartmentViewModels;

namespace Web.Controllers
{
    public class DepartmentController : Controller
    {
        public readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var department=_departmentService.GetAll();
            return View(department);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.EmployeeList = _departmentService.GetAllEmployeeName();
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddDepartmentViewModel model)
        {
            var isAdded = _departmentService.Create(model);
            if (isAdded)
            {
                return RedirectToAction("Index");   
            }
            else
            {
                TempData["MESSAGE_OF_ADD"] = "Failed to Add this department because there are already a department of this Department Name";
                return RedirectToAction("Add");
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _departmentService.GetById(id);
            ViewBag.EmployeeList = _departmentService.GetAllEmployeeName();
            return View(viewModel);
        }
        public IActionResult Update(UpdateDepartmentViewModel model)
        {
            var isUpdate = _departmentService.Update(model);
            if (isUpdate)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", new { id = model.ManagerId });
            }
        }
        public IActionResult Delete(int id) 
        {
            var isDeleted = _departmentService.Delete(id);
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
            bool isSoftDeleted = _departmentService.SoftDelete(id);
            if (isSoftDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MESSAGE"] = "Failed to delete this department because there are employees in this department";
                return RedirectToAction("Index");
            }
        }
    }
}
