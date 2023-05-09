using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels.BenefitViewModels;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BenefitController : ControllerBase
    {
        public readonly IBenefitService _benefitService;
        public BenefitController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_benefitService.GetAll());
        }
        [HttpPost]
        public IActionResult Add(AddBenefitViewModel addBenefitViewModel)
        {
            bool isAdded = _benefitService.Create(addBenefitViewModel);
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
        public IActionResult Get(int id)
        {
            return Ok(_benefitService.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(UpdateBenefitViewModel model)
        {
            var isUpdate = _benefitService.Update(model);
            if (isUpdate)
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
            var isDeleted = _benefitService.Delete(id);
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
            bool isSoftDeleted = _benefitService.SoftDelete(id);
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
