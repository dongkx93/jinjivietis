using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Login;

namespace WebApplication2.Common.ControllerCommon
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            InitAppController(filterContext);
            base.OnActionExecuting(filterContext);
        }

        public void InitAppController(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["employee"] == null)
            {
                filterContext.Result = RedirectToAction("Index", "Login");
            }
            else
            {
                EmployeeLoginOutput employee = (EmployeeLoginOutput)filterContext.HttpContext.Session["employee"];
                if(!employee.AuthorityId.Equals(ConstantCommon.ADMIN))
                {
                    filterContext.Result = RedirectToAction("Index", "Login");
                }
            }
        }
    }
}