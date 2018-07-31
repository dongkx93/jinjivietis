using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Login;

namespace WebApplication2.Common.ControllerCommon
{
    public class UserController : Controller
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
        }
    }
}