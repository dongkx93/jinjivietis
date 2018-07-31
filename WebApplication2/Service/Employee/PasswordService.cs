using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Common;

namespace WebApplication2.Service.Employee
{
    public class PasswordService
    {
        public void ChangePassword(string employeeId ,string password,string newPassword)
        {
            string hashPassword = HelperCommon.hashPassword(password);
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee
                             where e.employeeId.Equals(employeeId) && e.passWord.Equals(hashPassword)
                             select e).FirstOrDefault();
                if(query == null)
                {
                    throw new Exception("パスワードが間違いで、再入力ください");
                }
                query.passWord = HelperCommon.hashPassword(newPassword);
                db.SaveChanges();
            }
        }
    }
}