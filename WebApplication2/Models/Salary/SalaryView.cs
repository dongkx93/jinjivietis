using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Salary
{
    public class SalaryView
    {
        private string _employeeId;
        private string _name;
        private string _totalPaidAmount;
        private string _deductionAmount;
        private string _paidAmount;
        private string _salaryDetailFilePath;
        private string _fileName;
        private string _description;

        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string TotalPaidAmount { get; set; }
        public string DeductionAmount { get; set; }
        public string PaidAmount { get; set; }
        public string SalaryDetailFilePath { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }
}