using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Implementations;
using Service.Interfaces;
using Service.ViewModels.BenefitViewModels;

namespace Web.Controllers;
public class BenefitController : Controller
{
   public readonly IBenefitService _benefitService;
    public BenefitController(IBenefitService benefitService)
    {
        _benefitService = benefitService;
    }

    [HttpGet]
    public IActionResult Index()
    {
       var benefit= _benefitService.GetAll();
        return View(benefit);
    }
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.EmployeeList = _benefitService.GetAllEmployeeName();    
       return View();
    }
    [HttpPost]
    public IActionResult Add(AddBenefitViewModel addBenefitViewModel)
    {
        bool isAdded = _benefitService.Create(addBenefitViewModel);
        if(isAdded)
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
       var viewModel=_benefitService.GetById(id);
        ViewBag.EmployeeList = _benefitService.GetAllEmployeeName();
        return View(viewModel);
    }
    [HttpPost]
    public IActionResult Edit(UpdateBenefitViewModel model)
    {
        var isUpdate=_benefitService.Update(model);
        if (isUpdate)
        {
            return RedirectToAction("Index");
        }
        else 
        {
           return RedirectToAction("Edit", new {id=model.BenefitsId});
        }  
    }
   
    public IActionResult Delete(int id)
    {
       var isDeleted= _benefitService.Delete(id);   
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
        bool isSoftDeleted = _benefitService.SoftDelete(id);
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
