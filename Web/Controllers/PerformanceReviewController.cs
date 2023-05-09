using Microsoft.AspNetCore.Mvc;
using Entity.Models;
using Repository.Context;
using Service.ViewModels.Common;
using Service.ViewModels.PerformanceReviewViewModels;
using System.Xml.Linq;
using Service.Interfaces;
using Service.Implementations;

namespace Web.Controllers
{
    public class PerformanceReviewController : Controller
    {
        public readonly IPerformanceReviewService _performanceReviewService;
        public PerformanceReviewController(IPerformanceReviewService performanceReviewService)
        {
            _performanceReviewService = performanceReviewService;
        }

        public IActionResult Index()
        {
            var performanceReview = _performanceReviewService.GetAll();
            return View(performanceReview);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.EmployeeList = _performanceReviewService.GetAllEmployeeName();
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddPerformanceReviewViewModels addPerformanceReviewViewModels)
        {
            var IsAdded = _performanceReviewService.Create(addPerformanceReviewViewModels);
            if (IsAdded)
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
            var viewModel = _performanceReviewService.GetById(id);
            ViewBag.EmployeeList = _performanceReviewService.GetAllEmployeeName();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(UpdatePerformanceReviewViewModels model)
        {
            var isUpdated= _performanceReviewService.Update(model); 
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", new { id = model.PerformanceReviewId });
            }
        }

        public IActionResult Delete(int id)
        {
            var isDeleted=_performanceReviewService.Delete(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult SoftDelete(int id)
        {
            bool isSoftDeleted = _performanceReviewService.SoftDelete(id);
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
