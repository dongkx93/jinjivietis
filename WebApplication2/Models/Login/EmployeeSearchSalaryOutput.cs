using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Login
{
    public class EmployeeSearchSalaryOutput
    {
        private string _salaryId;
        private string _employeeId;
        private string _paymentDate;
        private string _salaryDetailFilePath;
        private string _totalPaidAmount;
        private string _deductionAmount;
        private string _paidAmount;
        private string _description;

        public string SalaryId { get; set; }
        public string EmployeeId { get; set; }
        public string PaymentDate { get; set; }
        public string SalaryDetailFilePath { get; set; }
        public string TotalPaidAmount { get; set; }
        public string DeductionAmount { get; set; }
        public string PaidAmount { get; set; }
        public string Description { get; set; }
    }
}