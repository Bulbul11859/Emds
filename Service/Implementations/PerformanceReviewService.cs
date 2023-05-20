using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.UnitOfWorks;
using Service.Interfaces;
using Service.ViewModels.Common;
using Service.ViewModels.PerformanceReviewViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations;

public class PerformanceReviewService : IPerformanceReviewService
{
    public readonly IUnitOfWork _unitOfWork;
    public ILogger<PerformanceReviewService> _logger;
    public PerformanceReviewService(IUnitOfWork unitOfWork, ILogger<PerformanceReviewService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public bool Create(AddPerformanceReviewViewModels model)
    {
        try
        {
            var record = _unitOfWork.PerformanceReview.FirstOrDefault(x => x.EmployeeId == model.EmployeeId);
            if (record != null)
            {
                record.IsDeleted = false;
                _unitOfWork.SaveChangesAsync();
            }
            else if (model != null && model.EmployeeId != model.ReviewerId && record == null)
            {
                var performanceReview = new PerformanceReview();
                performanceReview.PerformanceReviewId = model.PerformanceReviewId;
                performanceReview.EmployeeId = model.EmployeeId;
                performanceReview.ReviewerId = model.ReviewerId;
                performanceReview.OverallRating = model.OverallRating;
                performanceReview.Comments = model.Comments;
                performanceReview.CreationDate = DateTime.Now;
                _unitOfWork.PerformanceReview.Create(performanceReview);
                _unitOfWork.SaveChangesAsync();

            }
            return true;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var performanceReview = _unitOfWork.PerformanceReview.GetById(id);
            if (performanceReview != null)
            {
                _unitOfWork.PerformanceReview.Delete(performanceReview);
                _unitOfWork.SaveChangesAsync();
            }
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public List<PerformanceListViewModel> GetAll()
    {
        try
        {
            var employee = _unitOfWork.Employee.GetAllEnumerable();
            var performanceReview = _unitOfWork.PerformanceReview
                .Include(i=>i.Employee)
                .Where(i =>!i.IsDeleted && !i.Employee.IsDeleted)
                .AsEnumerable()
                .Select(x => new PerformanceListViewModel
            {
                PerformanceReviewId = x.PerformanceReviewId,
                EmployeeFirstName = x.EmployeeId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.EmployeeId).FirstName) : "",
                EmployeeLastName = x.EmployeeId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.EmployeeId).LastName) : "",
                ReviewrFirstName = x.ReviewerId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.ReviewerId).FirstName) : "",
                ReviewrLastName = x.ReviewerId != null ? (employee.FirstOrDefault(e => e.EmployeeId == x.ReviewerId).LastName) : "",
                OverallRating = x.OverallRating,
                Comments = x.Comments

            }).ToList();
            return performanceReview;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new List<PerformanceListViewModel>();
        }
    }

    public List<Dropdown> GetAllEmployeeName()
    {
        try
        {
            var employelist = _unitOfWork.Employee.Where(i => i.IsDeleted != true).Select(x => new Dropdown
            {
                Id = x.EmployeeId,
                Name = x.FirstName + " " + x.LastName
            }).ToList();
            return employelist;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new List<Dropdown>();
        }
    }

    public UpdatePerformanceReviewViewModels GetById(int id)
    {
        try
        {
            var performanceReview = _unitOfWork.PerformanceReview.FirstOrDefault(x => x.PerformanceReviewId == id);
            var viewModel = new UpdatePerformanceReviewViewModels();
            if (performanceReview != null)
            {
                viewModel.PerformanceReviewId = performanceReview.PerformanceReviewId;
                viewModel.EmployeeId = performanceReview.EmployeeId;
                viewModel.ReviewerId = performanceReview.ReviewerId;
                viewModel.OverallRating = performanceReview.OverallRating;
                viewModel.Comments = performanceReview.Comments;
            }
            return viewModel;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return new UpdatePerformanceReviewViewModels();
        }
    }

    public bool SoftDelete(int id)
    {
        try
        {
            var performanceReview = _unitOfWork.PerformanceReview.GetById(id);

            if (performanceReview != null)
            {
                performanceReview.IsDeleted = true;

                _unitOfWork.SaveChangesAsync();

            }
            return true;
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool Update(UpdatePerformanceReviewViewModels model)
    {
        try
        {
            var performanceReview = _unitOfWork.PerformanceReview.GetById(model.PerformanceReviewId);
            if (performanceReview != null && model.EmployeeId != model.ReviewerId)
            {
                performanceReview.EmployeeId = model.EmployeeId;
                performanceReview.ReviewerId = model.ReviewerId;
                performanceReview.OverallRating = model.OverallRating;
                performanceReview.Comments = model.Comments;
                performanceReview.UpdatedDate = DateTime.Now;
                _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;

        }
    }
}
