using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModels.DepartmentViewModels;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_departmentService.GetAll());
        }
        [HttpPost]
        public IActionResult Add(AddDepartmentViewModel model)
        {
            var isAdded = _departmentService.Create(model);
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
            return Ok(_departmentService.GetById(id));
        }
        [HttpPost]
        public IActionResult Update(UpdateDepartmentViewModel model)
        {
            var isUpdate = _departmentService.Update(model);
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
            var isDeleted = _departmentService.Delete(id);
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
            bool isSoftDeleted = _departmentService.SoftDelete(id);
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
