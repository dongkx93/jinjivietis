using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Common;
using WebApplication2.Models.Login;

namespace WebApplication2.Service.Employee
{
    public class LoginService
    {
        public EmployeeLoginOutput Login(EmployeeLoginInput employeeLoginInput)
        {
            EmployeeLoginOutput employeeLoginOutput = new EmployeeLoginOutput();
            try
            { 
                string passWord = HelperCommon.hashPassword(employeeLoginInput.PassWord);
                using (sys_employeeEntities db = new sys_employeeEntities())
                {
                    var query = (from e in db.employee
                                 join c in db.customer
                                 on e.customerId equals c.customerId into ecGroup
                                 from ec in ecGroup.DefaultIfEmpty()
                                 where e.userName.Equals(employeeLoginInput.UserName) && e.passWord.Equals(passWord)
                                 select new
                                 {
                                     e.employeeId,
                                     e.name,
                                     e.kataName,
                                     e.mailAddress,
                                     e.telephoneNumber,
                                     e.entryDate,
                                     customerName = ec.name,
                                     e.address,
                                     e.accountBankInfo,
                                     e.personalNumber,
                                     e.dateOfBirth,
                                     e.authorityId,
                                     e.avatarFilePath
                                 }).FirstOrDefault();
                    if (query == null)
                    {
                        throw new Exception("ユーザネームまたパスワードが違います");
                    }

                    employeeLoginOutput.Id = query.employeeId;
                    employeeLoginOutput.Name = query.name;
                    employeeLoginOutput.KataName = query.kataName;
                    employeeLoginOutput.Email = query.mailAddress;
                    employeeLoginOutput.TelephoneNumber = query.telephoneNumber;
                    employeeLoginOutput.EntryDate = query.entryDate;
                    employeeLoginOutput.CustomerName = query.customerName;
                    employeeLoginOutput.Address = query.address;
                    employeeLoginOutput.AccountBankInfo = query.accountBankInfo;
                    employeeLoginOutput.PersonalNunber = query.personalNumber;
                    employeeLoginOutput.DateOfBirth = query.dateOfBirth.GetValueOrDefault().ToString("yyyy/MM/dd");
                    employeeLoginOutput.AuthorityId = query.authorityId;
                    employeeLoginOutput.AvatarFilePath = query.avatarFilePath;

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return employeeLoginOutput;
        }
     }
}