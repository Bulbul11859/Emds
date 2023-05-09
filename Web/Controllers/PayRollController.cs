using Microsoft.AspNetCore.Mvc;
using Service.Implementations;
using Service.Interfaces;
using Service.ViewModels.PayRollViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web.Controllers
{
    public class PayRollController : Controller
    {
        public readonly IPayRollService _payRollService;
        public PayRollController(IPayRollService payRollService)
        {
            _payRollService = payRollService;
        }
        public IActionResult Index()
        {
            var payroll = _payRollService.GetAll();
            return View(payroll);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.EmployeeList=_payRollService.GetAllEmployeeName();
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddPayRollViewModel model)
        {
           var payroll=_payRollService.Create(model);
            if (payroll)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Add");
            }
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var payroll=_payRollService.GetById(id);
            ViewBag.EmployeeList = _payRollService.GetAllEmployeeName();
            return View(payroll);  
        }
        [HttpPost]
        public IActionResult Update(UpdatePayRollViewModel model)
        {
            var isUpdated = _payRollService.Update(model);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", new { id = model.PayrollId });
            }
        }
        public IActionResult Delete(int id)
        {
            var isDeleted = _payRollService.Delete(id);
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
            bool isSoftDeleted = _payRollService.SoftDelete(id);
            if (isSoftDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
