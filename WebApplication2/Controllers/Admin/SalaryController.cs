using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication2.Common;
using WebApplication2.Common.ControllerCommon;
using WebApplication2.Models.Salary;
using WebApplication2.Service.Admin;

namespace WebApplication2.Controllers.Admin
{
    public class SalaryController : AdminController
    {
        // GET: Salary
        [HttpGet]
        public ActionResult Index()
        {
            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            if (Session["SalaryMonth"] == null)
            {
                Session["SalaryMonth"] = String.Format("{0}/{1}/{2}",DateTime.Now.Year,DateTime.Now.Month.ToString("00"),"01");
            }
            if (TempData["salarys"] != null)
            {
                ViewData["salarys"] = TempData["salarys"];
            }

            if(TempData["SearchMonth"] != null)
            {
                ViewData["SearchMonth"] = TempData["salaryMonth"].ToString().Substring(0 , 7);
            }
            return View("~/Views/Salary/Index.cshtml");
        }

        [HttpParamAction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchSalary(string SalaryMonth)
        {
            Session["SalaryMonth"] = SalaryMonth;
            SalaryService salaryService = new SalaryService();
            List<SalaryView> salarys = salaryService.getSalaryByMonth(SalaryMonth);
            TempData["salarys"] = salarys;
            TempData["salaryMonth"] = SalaryMonth;
            TempData["ViewData"] = ViewData;
            return RedirectToAction("Index");
        }

        [HttpParamAction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadSalary(HttpPostedFileBase SalaryDetailZipFile , string SalaryMonth)
        {
            try
            {
                if (SalaryDetailZipFile == null)
                {
                    ModelState.AddModelError(string.Empty, "Zipfileアップロードください");
                    return SearchSalary(SalaryMonth);
                }
                string salaryDetailFileExtension = Path.GetExtension(SalaryDetailZipFile.FileName);
                if (!salaryDetailFileExtension.Equals(".zip"))
                {
                    ModelState.AddModelError(string.Empty, "Zipfileアップロードください");
                    return SearchSalary(SalaryMonth);
                }
                SalaryService salaryService = new SalaryService();
                salaryService.uploadSalaryDetailZipFiles(SalaryMonth, SalaryDetailZipFile);
                TempData["SuccessMessage"] = "アップロード成功";
                return SearchSalary(SalaryMonth);
            }catch(Exception e)
            {
                ModelState.AddModelError(string.Empty,e.Message);
                return SearchSalary(SalaryMonth);
            }
        }

        [HttpPost]
        public ActionResult SalaryUpload(string SalaryMonth)
        {
            Session["SalaryMonth"] = SalaryMonth;
            return View("~/Views/Salary/SalaryUpload.cshtml");
        }

        public ActionResult SalaryUploadView()
        {
            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if(TempData["SuccessMessage"]!= null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }
            
            return View("~/Views/Salary/SalaryUpload.cshtml");
        }

        [HttpPost]
        public ActionResult UploadSalaryDetailFile(string SalaryMonth , HttpPostedFileBase SalaryDetailFile)
        {
            try
            {
                if (SalaryDetailFile == null)
                {  
                    ModelState.AddModelError(string.Empty, "PDFfileアップロードください");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("SalaryUploadView");
                }
                string salaryDetailFileExtension = Path.GetExtension(SalaryDetailFile.FileName);
                if (!salaryDetailFileExtension.Equals(".pdf"))
                {
                    ModelState.AddModelError(string.Empty, "PDFfileアップロードください");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("SalaryUploadView");
                }
                SalaryService salaryService = new SalaryService();
                string salaryFileName = salaryService.uploadSalaryDetailFile(SalaryMonth, SalaryDetailFile);
                TempData["SuccessMessage"] = "アップロード成功";
                return RedirectToAction("SalaryUploadView");
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                TempData["ViewData"] = ViewData;
                return RedirectToAction("SalaryUploadView");
            }
            
        }

        
    }
}