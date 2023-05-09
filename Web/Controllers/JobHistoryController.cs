using Microsoft.AspNetCore.Mvc;
using Service.Implementations;
using Service.Interfaces;
using Service.ViewModels.JobHistoryViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web.Controllers
{
    public class JobHistoryController : Controller
    {
        public readonly IJobHistoryService _jobHistoryService;
        public JobHistoryController(IJobHistoryService jobHistoryService)
        {
            _jobHistoryService = jobHistoryService;
        }
        public IActionResult Index()
        {
            var jobHistory = _jobHistoryService.GetAll();
            return View(jobHistory);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.EmployeeList=_jobHistoryService.GetAllEmployeeName();
            return View();  
        }
        [HttpPost]
        public IActionResult Add(AddJobHistoryViewModel model)
        {
            var isAdded = _jobHistoryService.Create(model);
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
        public IActionResult Edit(int id) 
        {
            var viewModel = _jobHistoryService.GetById(id);
            ViewBag.EmployeeList = _jobHistoryService.GetAllEmployeeName();
            return View(viewModel);  
        }
        [HttpPost]
        public IActionResult Update(UpdateJobHistoryViewModel model)
        {
            var isUpdated= _jobHistoryService.Update(model);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", new { id = model.JobHistoryId });
            }
        }

        public IActionResult Delete(int id)
        {
            var isDeleted = _jobHistoryService.Delete(id);
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
            bool isSoftDeleted = _jobHistoryService.SoftDelete(id);
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
