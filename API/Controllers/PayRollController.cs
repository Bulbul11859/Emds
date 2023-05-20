using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels.PayRollViewModels;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PayRollController : ControllerBase
    {
        public readonly IPayRollService _payRollService;
        public PayRollController(IPayRollService payRollService)
        {
            _payRollService = payRollService;
        }
        [HttpGet] 
        public IActionResult Index()
        {
            return Ok(_payRollService.GetAll());
        }
        [HttpPost]
        public IActionResult Add(AddPayRollViewModel model)
        {
            var payroll = _payRollService.Create(model);
            if (payroll)
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
            return Ok(_payRollService.GetById(id));
        }
        [HttpPost]
        public IActionResult Update(UpdatePayRollViewModel model)
        {
            var isUpdated = _payRollService.Update(model);
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
            var isDeleted = _payRollService.Delete(id);
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
            bool isSoftDeleted = _payRollService.SoftDelete(id);
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
