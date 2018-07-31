using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Login;

namespace WebApplication2.Service.Employee
{
    public class SalaryService
    {
        public EmployeeSearchSalaryOutput getSalaryDetailEmployee(string employeeId, string salaryMonth)
        {
            EmployeeSearchSalaryOutput employeeSearchSalaryOutput = new EmployeeSearchSalaryOutput();
            string salaryId = String.Format("{0}{1}", employeeId, salaryMonth.Substring(0, 7).Replace("/", ""));
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from s in db.salary
                             where s.salaryId.Equals(salaryId)
                             select new
                             {
                                 s.salaryId,
                                 s.employeeId,
                                 s.paymentDate,
                                 s.salaryDetailFilePath,
                                 s.totalPaidAmount,
                                 s.deductionAmount,
                                 s.paidAmount
                             }).FirstOrDefault();
                if(query == null)
                {
                    return null;
                }
                employeeSearchSalaryOutput.SalaryId = query.salaryId;
                employeeSearchSalaryOutput.EmployeeId = query.employeeId;
                employeeSearchSalaryOutput.PaymentDate = query.paymentDate;
                employeeSearchSalaryOutput.SalaryDetailFilePath = query.salaryDetailFilePath;
                employeeSearchSalaryOutput.TotalPaidAmount = query.totalPaidAmount == null ? "" : query.totalPaidAmount.ToString();
                employeeSearchSalaryOutput.DeductionAmount = query.deductionAmount == null ? "" : query.deductionAmount.ToString();
                employeeSearchSalaryOutput.PaidAmount = query.paidAmount == null ? "" : query.paidAmount.ToString();

                return employeeSearchSalaryOutput;
            }
        }
    }
}