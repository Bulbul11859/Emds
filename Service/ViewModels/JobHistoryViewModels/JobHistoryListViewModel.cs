using Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.JobHistoryViewModels
{
    public class JobHistoryListViewModel
    {
      
        public int JobHistoryId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeFullName
        {
            get
            {
                return $"{EmployeeFirstName} {EmployeeLastName}";
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }
        public string Responsibilities { get; set; }
    }
}
