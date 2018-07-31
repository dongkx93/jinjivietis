using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Common;
using WebApplication2.Models.Login;
using WebApplication2.Service.Employee;

namespace WebApplication2.Controllers
{
    public class LoginController:Controller
    {
        public ActionResult Index()
        {
            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            if(TempData["employee"] != null)
            {
                EmployeeLoginInput employeeLoginInput = (EmployeeLoginInput)TempData["employee"];
                return View("~/Views/Login/Index.cshtml" , employeeLoginInput);
            }
            return View("~/Views/Login/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Login(EmployeeLoginInput employeeLoginInput)
        {
            try
            {
                if(ModelState.IsValid) {
                    LoginService loginService = new LoginService();
                    EmployeeLoginOutput employeeLoginOutput = loginService.Login(employeeLoginInput);
                    Session["employee"] = employeeLoginOutput;
                    return RedirectToAction("ViewEmployeeInfo");
                }
            }
            catch(Exception e) {
                ModelState.AddModelError(string.Empty, e.Message);
                TempData["ViewData"] = ViewData;
                TempData["employee"] = employeeLoginInput;
                return RedirectToAction("Index");
            }
            TempData["ViewData"] = ViewData;
            TempData["employee"] = employeeLoginInput;
            return RedirectToAction("Index");
        }

        public ActionResult ViewEmployeeInfo()
        {
            
            if (Session["employee"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return DecideRoute();
            }
        }

        

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            InitAppController(filterContext);
            base.OnActionExecuting(filterContext);
        }

        public void InitAppController(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Session["employee"] != null)
            {
                filterContext.Result = DecideRoute();
            }
        }

        public ActionResult DecideRoute()
        {
            EmployeeLoginOutput employeeLoginOutput = (EmployeeLoginOutput)Session["employee"];
            string authorityId = employeeLoginOutput.AuthorityId;
            if (authorityId.Equals(ConstantCommon.ADMIN))
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return RedirectToAction("Index", "Info");
            }
        }
    }
}