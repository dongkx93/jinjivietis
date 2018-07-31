using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Common
{
    public class DataBaseCommon
    {
        public static Dictionary<string , string> getManager()
        {
            Dictionary<string , string> managers = new Dictionary<string, string>();
            managers.Add("", "");
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee
                             where e.authorityId.Equals(ConstantCommon.MANAGER)
                             select new
                             {
                                 e.employeeId,
                                 e.name
                             }).ToList();

                foreach(var item in query)
                {
                    managers.Add(item.name , item.employeeId);
                }
            }
                return managers;
        }

        public static Dictionary<string , string> getAuthority()
        {
            Dictionary<string, string> authoritys = new Dictionary<string, string>();
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from a in db.authority
                             select new
                             {
                                a.authorityId,
                                a.authorityName
                             }).ToList();
                foreach (var item in query)
                {
                    authoritys.Add(item.authorityName, item.authorityId);
                }
            }
                return authoritys;
        }

        public static Dictionary<string , string> getCustomer()
        {
            Dictionary<string, string> customers = new Dictionary<string, string>();
            customers.Add("", "");
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from c in db.customer
                             select new
                             {
                                 c.customerId,
                                 c.name
                             }).ToList();
                foreach (var item in query)
                {
                    customers.Add(item.name, item.customerId);
                }
            }
            return customers;
        }
        public static bool isDuplicateEmployee(string employeeId, sys_employeeEntities db)
        {
            var query = (from e in db.employee
                         where e.employeeId.Equals(employeeId)
                         select new
                         {
                             e.employeeId
                         }).FirstOrDefault();
            if (query == null)
            {
                return false;
            }
            return true;
        }

        public static string getEmployeeUsername(string employeeId , sys_employeeEntities db)
        {
            var query = (from e in db.employee
                         where e.employeeId.Equals(employeeId)
                         select new
                         {
                             e.userName
                         }).FirstOrDefault();
            string employeeUsername = query.userName;

            return employeeUsername;
        }

        public static bool isDuplicateUsername(string username, sys_employeeEntities db)
        {
            var query = (from e in db.employee
                         where e.userName.Equals(username)
                         select e).FirstOrDefault();

            if(query == null)
            {
                return false;
            }
            return true;
        }

        public static bool isEmployeeExist(string id, string name , sys_employeeEntities db)
        {
            var query = (from e in db.employee
                            where e.employeeId == id && e.name == name
                            select new
                            {
                                e.employeeId
                            }).Count();
            if (query == 0)
            {
                return false;
            }
            
                return true;
        }

        public static bool isSalaryExist(string salaryId , sys_employeeEntities db)
        {
            var query = (from s in db.salary 
                         where s.salaryId == salaryId
                         select new
                         {
                             s.salaryId
                         }
                         ).Count();
            if(query == 0)
            {
                return false;
            }

            return true;
        }

    }
}