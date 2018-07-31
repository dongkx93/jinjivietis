using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Common;
using WebApplication2.Common.ControllerCommon;
using WebApplication2.Models.Login;
using WebApplication2.Service.Employee;

namespace WebApplication2.Controllers.Employee
{
    public class InfoController : UserController
    {
        // GET: Info
        public ActionResult Index(EmployeeLoginInput employeeLoginInput)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            if (TempData["SalaryMonth"] == null)
            {
                TempData["SalaryMonth"] = String.Format("{0}/{1}/{2}", DateTime.Now.Year, DateTime.Now.Month.ToString("00"), "01");
            }
            return View("~/Views/Employee/Index.cshtml");
        }

        [HttpPost]
        public ActionResult GetEmployeeSalaryMonth(string SalaryMonth)
        {
            try
            {
                if (null == SalaryMonth || "".Equals(SalaryMonth))
                {
                    ModelState.AddModelError(string.Empty,"年月を入力ください");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }
                TempData["SalaryMonth"] = SalaryMonth;
                EmployeeLoginOutput sessionEmployee = (EmployeeLoginOutput)Session["employee"];
                string employeeId = sessionEmployee.Id;
                SalaryService salaryService = new SalaryService();

                EmployeeSearchSalaryOutput employeeSearchSalaryOutput = salaryService.getSalaryDetailEmployee(employeeId, SalaryMonth);
                if (employeeSearchSalaryOutput == null)
                {
                    ModelState.AddModelError(string.Empty, "当月、給料明細書まだアップロードされません");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }
                TempData["SalaryDetailFilePath"] = employeeSearchSalaryOutput.SalaryDetailFilePath;
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PasswordChange(string NewPassword, string Password,string ConfirmPassword)
        {
            try
            {
                if(NewPassword.Length < 6 || Password.Length < 6 || ConfirmPassword.Length < 6)
                {
                    ModelState.AddModelError(string.Empty, "パスワードの長さが最低6桁をに入力ください。");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("PasswordChange");
                }
                if(!Password.Equals(ConfirmPassword))
                {
                    ModelState.AddModelError(string.Empty , "パスワードと確認パスワードが同じではないです。");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("PasswordChange");
                }
                PasswordService passwordService = new PasswordService();
                EmployeeLoginOutput employee = (EmployeeLoginOutput)Session["employee"];
                string employeeId = employee.Id;
                passwordService.ChangePassword(employeeId , Password , NewPassword);
                TempData["SuccessMessage"] = "パスワード変更ができました";
                TempData["ViewData"] = ViewData;
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty , e.Message);
                TempData["ViewData"] = ViewData;
                return RedirectToAction("PasswordChange");
            }
            return RedirectToAction("PasswordChange");
        }

        public ActionResult PasswordChange()
        {
            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            return View("~/Views/Employee/PasswordChange.cshtml");
        }
    }
}