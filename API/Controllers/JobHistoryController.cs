using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels.JobHistoryViewModels;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobHistoryController : ControllerBase
    {
        public readonly IJobHistoryService _jobHistoryService;
        public JobHistoryController(IJobHistoryService jobHistoryService)
        {
            _jobHistoryService = jobHistoryService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_jobHistoryService.GetAll());
        }
        [HttpPost]
        public IActionResult Add(AddJobHistoryViewModel model)
        {
            var isAdded = _jobHistoryService.Create(model);
            if (isAdded)
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
            return Ok(_jobHistoryService.GetById(id));
        }
        [HttpPost]
        public IActionResult Update(UpdateJobHistoryViewModel model)
        {
            var isUpdated = _jobHistoryService.Update(model);
            if (isUpdated)
            {
                return Ok("Successfully Updated");
            }
            else
            {
                return BadRequest("Failed to Update");
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _jobHistoryService.Delete(id);
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
            bool isSoftDeleted = _jobHistoryService.SoftDelete(id);
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
