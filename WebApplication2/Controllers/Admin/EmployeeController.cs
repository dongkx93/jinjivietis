using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Common;
using WebApplication2.Common.ControllerCommon;
using WebApplication2.Models.Employee;
using WebApplication2.Service;

namespace WebApplication2.Controllers
{
    public class EmployeeController : AdminController
    {
        public ActionResult Index()
        {
            EmployeeService employeeService = new EmployeeService();
            List<SelectListItem> customers = HelperCommon.convertToListItem(DataBaseCommon.getCustomer());
            List<SelectListItem> authorities = HelperCommon.convertToListItem(DataBaseCommon.getAuthority());
            authorities.Insert(0, new SelectListItem{Text = "" , Value = ""});

            ViewBag.authorities = authorities;

            ViewBag.customers = customers;

            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if(TempData["employees"] != null)
            {
                ViewData["employees"] = TempData["employees"];
            } else
            {
                List<EmployeeSearchOutput> employees = employeeService.getEmployees();
                ViewData["employees"] = employees;
            }

            if(TempData["employeeSearchInput"] != null)
            {
                var model = TempData["employeeSearchInput"];
                return View("~/Views/Admin/Index.cshtml" , model);
            }
            
            return View("~/Views/Admin/Index.cshtml");
        }

        public ActionResult EmployeeSearch(EmployeeSearchInput employeeSearchInput)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                List<EmployeeSearchOutput> employees = employeeService.searchEmployees(employeeSearchInput);
                TempData["employees"] = employees;
                TempData["employeeSearchInput"] = employeeSearchInput;
                return RedirectToAction("Index");
            }catch(Exception e) {
                TempData["ViewData"] = ViewData;
                TempData["employeeSearchInput"] = employeeSearchInput;
                return RedirectToAction("Index");
            }
        }

        public ActionResult EmployeeAdd()
        {
            ViewBag.Message = "Your application description page.";

            List<SelectListItem> authorities = HelperCommon.convertToListItem(DataBaseCommon.getAuthority());
            List<SelectListItem> managers = HelperCommon.convertToListItem(DataBaseCommon.getManager());
            List<SelectListItem> customers = HelperCommon.convertToListItem(DataBaseCommon.getCustomer());
            TempData["authorities"] = authorities;
            TempData["managers"] = managers;
            TempData["customers"] = customers;

            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if(TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = (string)TempData["SuccessMessage"];
                TempData["SuccessMessage"] = null;
            }

            if (TempData["EmployeeAddInput"] != null)
            {
                var model = TempData["EmployeeAddInput"];
                return View("~/Views/Admin/EmployeeAdd.cshtml");
            }

            return View("~/Views/Admin/EmployeeAdd.cshtml");
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeAddInput employeeAddInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeService employeeService = new EmployeeService();
                    employeeService.addEmployee(employeeAddInput);
                    TempData["SuccessMessage"] = "社員追加が成功です";
                    return RedirectToAction("EmployeeAdd");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty,e.Message);
                TempData["ViewData"] = ViewData;
                TempData["EmployeeAddInput"] = employeeAddInput;
                return RedirectToAction("EmployeeAdd");
            }
            TempData["EmployeeAddInput"] = employeeAddInput;
            TempData["ViewData"] = ViewData;
            return RedirectToAction("EmployeeAdd");
        }

        public ActionResult EmployeeDetail(string id)
        {
            ViewBag.Message = "Your contact page.";
            
            List<SelectListItem> authorities = HelperCommon.convertToListItem(DataBaseCommon.getAuthority());
            List<SelectListItem> managers = HelperCommon.convertToListItem(DataBaseCommon.getManager());
            List<SelectListItem> customers = HelperCommon.convertToListItem(DataBaseCommon.getCustomer());
            TempData["authorities"]= authorities;
            TempData["managers"]= managers;
            TempData["customers"]= customers;

            EmployeeUpdateInput employee;

            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if(TempData["SuccesMessage"] != null)
            {
                ViewData["SuccesMessage"] = (string)TempData["SuccesMessage"];
            }

            if (TempData["EmployeeUpdateInput"] != null)
            {
                employee = (EmployeeUpdateInput)TempData["EmployeeUpdateInput"];
            } else
            {
                EmployeeService employeeService = new EmployeeService();
                employee = employeeService.getEmployeeUpdate(id);
            }

            HelperCommon.setSelected(authorities, employee.AuthorityId);
            HelperCommon.setSelected(managers, employee.ManagerId);
            HelperCommon.setSelected(customers,employee.CustomerId);

            return View("~/Views/Admin/EmployeeDetail.cshtml" , employee);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeUpdateInput employeeUpdateInput)
        {
            try{
                if (ModelState.IsValid)
                {
                    EmployeeService employeeService = new EmployeeService();
                    employeeService.updateEmployee(employeeUpdateInput);
                    TempData["SuccesMessage"] = "社員情報更新が成功です";
                    return RedirectToAction("EmployeeDetail" , new { id = employeeUpdateInput.Id });
                }
            }catch(Exception e)
            {
                ModelState.AddModelError(string.Empty ,e.Message);
                TempData["EmployeeUpdateInput"] = employeeUpdateInput;
                TempData["ViewData"] = ViewData;
                return RedirectToAction("EmployeeDetail", new { id = employeeUpdateInput.Id });
            }

            TempData["EmployeeUpdateInput"] = employeeUpdateInput;
            TempData["ViewData"] = ViewData;
            return RedirectToAction("EmployeeDetail", new { id = employeeUpdateInput.Id });
        }

        public ActionResult EmployeeDocument(string id)
        {
            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            EmployeeService employeeService = new EmployeeService();
            Dictionary<string , string> documents = employeeService.getPersonalDocument(id);
            ViewBag.Name = documents["name"];
            ViewBag.Id = id;
            documents.Remove("name");
            ViewData["documents"] = documents;
            return View("~/Views/Admin/EmployeeDocument.cshtml");
        }

        public ActionResult DeleteEmployeeDocument(string id)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                string filename = Request.QueryString["filename"];
                employeeService.deletePersonalDocument(id, filename);

                return RedirectToAction("EmployeeDocument", new { id = id });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                TempData["ViewData"] = ViewData;
                return RedirectToAction("EmployeeAdd");
            }
            
        }

        [HttpPost]
        public ActionResult AddEmployeeDocument(HttpPostedFileBase documentFile , string id)
        {
            try
            {
                if (documentFile == null)
                {
                    ModelState.AddModelError(string.Empty, "アップロードファイル選択ください");
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("EmployeeDocument", new { id = id });
                }
                EmployeeService employeeService = new EmployeeService();
                employeeService.addDocument(id, documentFile);
                return RedirectToAction("EmployeeDocument", new { id = id });
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                TempData["ViewData"] = ViewData;
                return RedirectToAction("EmployeeAdd");
            }
        }
    }
}