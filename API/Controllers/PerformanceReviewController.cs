using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels.PerformanceReviewViewModels;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PerformanceReviewController : ControllerBase
    {
        public readonly IPerformanceReviewService _performanceReviewService;
        public PerformanceReviewController(IPerformanceReviewService performanceReviewService)
        {
            _performanceReviewService = performanceReviewService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_performanceReviewService.GetAll());
        }
        [HttpPost]
        public IActionResult Add(AddPerformanceReviewViewModels addPerformanceReviewViewModels)
        {
            var IsAdded = _performanceReviewService.Create(addPerformanceReviewViewModels);
            if (IsAdded)
            {
                return Ok("Successfully Added");
            }
            else
            {
                return BadRequest("Failed to Add");
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return Ok(_performanceReviewService.GetById(id));
        }
        [HttpPost]
        public IActionResult Update(UpdatePerformanceReviewViewModels model)
        {
            var isUpdated = _performanceReviewService.Update(model);
            if (isUpdated)
            {
                return RedirectToAction("Successfully Updated");
            }
            else
            {
                return BadRequest("Failed to Update");
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _performanceReviewService.Delete(id);
            if (isDeleted)
            {
                return Ok("Successfully Deleted");
            }
            else
            {
                return BadRequest("Failed to delete this Employee because He/She is a Manager of Employee Table");
            }
        }
        [HttpDelete]
        public IActionResult SoftDelete(int id)
        {
            bool isSoftDeleted = _performanceReviewService.SoftDelete(id);
            if (isSoftDeleted)
            {
                return Ok("Successfully Deleted");
            }
            else
            {
                return BadRequest("Failed to delete this Employee because He/She is a Manager of Employee Table");
            }
        }
    }
}
