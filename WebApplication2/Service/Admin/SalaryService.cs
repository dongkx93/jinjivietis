using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication2.Common;
using WebApplication2.Models.Salary;

namespace WebApplication2.Service.Admin
{
    public class SalaryService
    {
        public List<SalaryView> getSalaryByMonth(string paymentDate)
        {
            List<SalaryView> salarys = new List<SalaryView>();

            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee
                             join s in db.salary
                             on e.employeeId equals s.employeeId into esGroup
                             from es in esGroup.DefaultIfEmpty()
                             where es.paymentDate.Substring(0,7).Equals(paymentDate.Substring(0,7))
                             select new
                             {
                                 es.employeeId ,
                                 e.name,
                                 es.totalPaidAmount,
                                 es.deductionAmount,
                                 es.paidAmount,
                                 es.salaryDetailFilePath,
                                 es.description
                             }).ToList();

                foreach(var item in query)
                {
                    SalaryView salary = new SalaryView();
                    salary.EmployeeId = item.employeeId;
                    salary.Name = item.name;
                    salary.TotalPaidAmount = item.totalPaidAmount.ToString();
                    salary.DeductionAmount = item.deductionAmount.ToString();
                    salary.PaidAmount = item.paidAmount.ToString();
                    salary.SalaryDetailFilePath = item.salaryDetailFilePath;
                    salary.FileName = item.salaryDetailFilePath.Substring(item.salaryDetailFilePath.LastIndexOf("\\") + 1);
                    salary.Description = item.description;
                    salarys.Add(salary);
                }
            }
            return salarys;
        }

        public bool uploadSalaryDetailZipFiles(string salaryMonth , HttpPostedFileBase salaryDetailFolderStream)
        {
            string salaryFolderName = salaryMonth.Substring(0, 7).Replace("/" , "");

            MemoryStream ms = new MemoryStream();
            salaryDetailFolderStream.InputStream.CopyTo(ms);
            string salaryDetailFolderPath = Path.Combine(ConstantCommon.SALARYFOLDERROOTPATH, salaryFolderName);
            List<FileStream> salaryDetailFileStreams = HelperCommon.extractZipfileToFileStream(ms , salaryDetailFolderPath);
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                foreach (FileStream salaryDetailFileStream in salaryDetailFileStreams)
                {
                    updateSalary(salaryMonth, salaryDetailFileStream, db);
                }
            }
                return false;
        }

        public string uploadSalaryDetailFile(string salaryMonth ,HttpPostedFileBase salaryDetailFileStream)
        {
            try
            {
                string salaryFileName = "";
                using (sys_employeeEntities db = new sys_employeeEntities())
                {
                    FileStream salaryFileStream = HelperCommon.saveSalaryDetail(salaryMonth, salaryDetailFileStream);
                    salaryFileName = updateSalary(salaryMonth, salaryFileStream, db);
                }

                return salaryFileName;
            } catch(Exception e)
            {
                throw e;
            }
            
                
        }

        public string updateSalary(string salaryMonth, FileStream salaryDetailFileStream , sys_employeeEntities db)
        {
            //FileStream salaryFileStream = HelperCommon.saveSalaryDetail(salaryMonth, salaryDetailFileStream);
            string salaryFilePath = salaryDetailFileStream.Name;
            FileInfo salaryFileInfo = new FileInfo(salaryFilePath);
            string salaryFileName = Path.GetFileNameWithoutExtension(salaryDetailFileStream.Name);
            string employeeId = String.Format("{0}{1}", "vis", salaryFileName.Substring(0, 3));
            string employeeName = salaryFileName.Substring(4);
            if (!salaryFileInfo.Extension.Equals(".pdf") || !DataBaseCommon.isEmployeeExist(employeeId, employeeName, db))
            {
                salaryDetailFileStream.Flush();
                salaryDetailFileStream.Close();
                File.Delete(salaryFileName);
                throw new Exception("PDFファイルではないまた社員存在しません");
            }
            salaryDetailFileStream.Flush();
            salaryDetailFileStream.Close();
            string salaryId = string.Format("{0}{1}", employeeId, salaryMonth.Substring(0, 7).Replace("/", ""));
            string salaryDetailFilePath = salaryFilePath.Substring(salaryFilePath.IndexOf(ConstantCommon.SALARYFOLDERPATH));
            if (DataBaseCommon.isSalaryExist(salaryId, db))
            {
                updateSalaryDetailFilePath(salaryId, salaryDetailFilePath, db);
            }
            else
            {
                addSalaryDetailFilePath(employeeId, salaryMonth, salaryDetailFilePath, db);
            }
            return salaryFileInfo.Name;
        }

        private bool updateSalaryDetailFilePath(string salaryId , string salaryDetailFilePath , sys_employeeEntities db)
        {
            var query = (from s in db.salary
                         where s.salaryId == salaryId
                         select s).First();
            salary salary = (salary)query;
            salary.salaryDetailFilePath = salaryDetailFilePath;

            db.SaveChanges();
            return false;
        }

        private bool addSalaryDetailFilePath(string employeeId , string salaryMonth , string salaryDetailFilePath , sys_employeeEntities db)
        {
            string salaryId = string.Format("{0}{1}",employeeId , salaryMonth.Substring(0,7).Replace("/" , ""));
            salary salary = new salary();
            salary.salaryId = salaryId;
            salary.employeeId = employeeId;
            salary.paymentDate = string.Format("{0}/{1}", salaryMonth.Substring(0, 7), "15");
            salary.salaryDetailFilePath = salaryDetailFilePath;//fileName.Substring(fileName.IndexOf(ConstantCommon.SALARYFOLDERPATH));
            salary.description = "";
            db.salary.Add(salary);
            db.SaveChanges();

            return false;
        }

        
        
    }
}