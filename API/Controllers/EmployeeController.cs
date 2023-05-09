using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels.EmployeeViewModels;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_employeeService.GetAll());
        }
        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel addEmployeeViewModel)
        {
            bool isAdded = _employeeService.Create(addEmployeeViewModel);
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
            return Ok(_employeeService.GetById(id));
        }
        [HttpPost]
        public IActionResult Update(UpdateEmployeeViewModel model)
        {
            bool isUpdate = _employeeService.Update(model);
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

            bool isDeleted = _employeeService.Delete(id);
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
            bool isSoftDeleted = _employeeService.SoftDelete(id);
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
