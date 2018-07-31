using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApplication2.Common;
using WebApplication2.Models.Employee;

namespace WebApplication2.Service
{
    public class EmployeeService
    {
        //社員リスト取得
        public List<EmployeeSearchOutput> getEmployees()
        {
            List<EmployeeSearchOutput> employees = new List<EmployeeSearchOutput>();
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee
                             join c in db.customer
                             on e.customerId equals c.customerId into ecGroup
                             from ec in ecGroup.DefaultIfEmpty()
                             join a in db.authority
                             on e.authorityId equals a.authorityId into ecaGroup
                             from eca in ecaGroup.DefaultIfEmpty()
                             where e.leavingDate == null
                             select new
                             {
                                 e.employeeId,
                                 e.name,
                                 e.kataName,
                                 e.telephoneNumber,
                                 e.mailAddress,
                                 customerName = ec.name,
                                 entryDate = e.entryDate,
                                 authority = eca.authorityName,
                                 e.description
                             })
                             .ToList();
                foreach(var item in query)
                {
                    EmployeeSearchOutput employee = new EmployeeSearchOutput();
                    employee.Id = item.employeeId;
                    employee.Name = item.name;
                    employee.KataName = item.kataName;
                    employee.TelephoneNumber = item.telephoneNumber;
                    employee.MailAddress = item.mailAddress;
                    employee.CustomerName = item.customerName;
                    employee.EntryDate = item.entryDate;
                    employee.Authority = item.authority;
                    employee.Description = item.description;

                    employees.Add(employee);
                }
            }

            return employees;
        }

        // 名前またIDで社員検索
        public List<EmployeeSearchOutput> searchEmployees(EmployeeSearchInput employeeSearchInput)
        {
            List<EmployeeSearchOutput> employees = new List<EmployeeSearchOutput>();
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee
                             join c in db.customer
                             on e.customerId equals c.customerId into ecGroup
                             from ec in ecGroup.DefaultIfEmpty()
                             join a in db.authority 
                             on e.authorityId equals a.authorityId into ecaGroup
                             from eca in ecaGroup.DefaultIfEmpty()
                             select new
                             {
                                 e.employeeId,
                                 e.name,
                                 e.kataName,
                                 e.telephoneNumber,
                                 e.mailAddress,
                                 customerName = ec.name,
                                 entryDate = e.entryDate,
                                 authority = eca.authorityName,
                                 eca.authorityId,
                                 e.leavingDate ,
                                 e.customerId,
                                 e.description
                             });
                if(!string.IsNullOrWhiteSpace(employeeSearchInput.Id))
                {
                    query = query.Where(p => p.employeeId.Equals(employeeSearchInput.Id));
                }

                if(!string.IsNullOrWhiteSpace(employeeSearchInput.Name))
                {
                    query = query.Where(p => p.name.Contains(employeeSearchInput.Name));
                }

                switch (employeeSearchInput.WorkSituation)
                {
                    case ConstantCommon.ZAISHA:
                        query = query.Where(p => p.leavingDate == null);
                        break;
                    case ConstantCommon.TAISHA:
                        query = query.Where(p => p.leavingDate != null);
                        break;
                    default:
                        break;
                }

                if(!string.IsNullOrWhiteSpace(employeeSearchInput.CustomerId))
                {
                    query = query.Where(p => p.customerId.Equals(employeeSearchInput.CustomerId));
                }

                if (!string.IsNullOrWhiteSpace(employeeSearchInput.AuthorityId))
                {
                    query = query.Where(p => p.authorityId.Equals(employeeSearchInput.AuthorityId));
                }

                query.ToList();

                foreach (var item in query)
                {
                    EmployeeSearchOutput employee = new EmployeeSearchOutput();
                    employee.Id = item.employeeId;
                    employee.Name = item.name;
                    employee.KataName = item.kataName;
                    employee.TelephoneNumber = item.telephoneNumber;
                    employee.MailAddress = item.mailAddress;
                    employee.CustomerName = item.customerName;
                    employee.EntryDate = item.entryDate;
                    employee.Authority = item.authority;
                    employee.Description = item.description;

                    employees.Add(employee);
                }
            }
            
            return employees;
        }

        // 社員追加
        public void addEmployee(EmployeeAddInput employee)
        {
            try
            {
                using (sys_employeeEntities db = new sys_employeeEntities())
                {
                    if (DataBaseCommon.isDuplicateEmployee(employee.Id, db))
                    {
                        throw new Exception("社員Idが重複発生です");
                    }
                    if(DataBaseCommon.isDuplicateUsername(employee.UserName , db))
                    {
                        throw new Exception("社員Usernameが重複発生です");
                    }
                    var path = "";
                    var avatarFile = employee.AvatarFile;
                    if (avatarFile != null && avatarFile.ContentLength > 0)
                    {
                        path = HelperCommon.saveAvatarFile(avatarFile, employee.Id);
                    }
                    employee employeeEntity = new employee();

                    employeeEntity.employeeId = employee.Id;
                    employeeEntity.managerId = employee.ManagerId;
                    employeeEntity.userName = employee.UserName;
                    employeeEntity.passWord = HelperCommon.hashPassword(employee.PassWord);
                    employeeEntity.authorityId = employee.AuthorityId;
                    employeeEntity.dateOfBirth = DateTime.Parse(employee.DateOfBirth);
                    employeeEntity.address = employee.Address;
                    employeeEntity.personalNumber = employee.PersonalNumber;
                    employeeEntity.name = employee.Name;
                    employeeEntity.kataName = employee.KataName;
                    employeeEntity.telephoneNumber = employee.TelephoneNumber;
                    employeeEntity.mailAddress = employee.MailAddress;
                    employeeEntity.customerId = employee.CustomerId;
                    employeeEntity.accountBankInfo = employee.AccountBankInfo;
                    employeeEntity.avatarFilePath = path;
                    employeeEntity.depentdentFamily = employee.DepentdentFamily;
                    employeeEntity.entryDate = employee.EntryDate;
                    employeeEntity.description = employee.Description;

                    db.employee.Add(employeeEntity);
                    db.SaveChanges();
                }
            } catch(Exception e)
            {
                throw e;
            }
        }

        //編集社員取得
        public EmployeeUpdateInput getEmployeeUpdate(string employeeId)
        {
            EmployeeUpdateInput employeeUpdate = new EmployeeUpdateInput();
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee where e.employeeId == employeeId
                             select new
                             {
                                 e.employeeId,
                                 e.name,
                                 e.kataName,
                                 e.authorityId,
                                 e.telephoneNumber,
                                 e.dateOfBirth,
                                 e.address,
                                 e.mailAddress,
                                 e.customerId,
                                 e.managerId,
                                 e.personalNumber,
                                 e.accountBankInfo,
                                 e.entryDate,
                                 e.leavingDate,
                                 e.depentdentFamily,
                                 e.userName,
                                 e.passWord,
                                 e.avatarFilePath,
                                 e.description
                             }).Single();
                if(query == null)
                {
                    return null;
                }
                employeeUpdate.Id = query.employeeId;
                employeeUpdate.Name = query.name;
                employeeUpdate.KataName = query.kataName;
                employeeUpdate.AuthorityId = query.authorityId;
                employeeUpdate.TelephoneNumber = query.telephoneNumber;
                employeeUpdate.DateOfBirth = query.dateOfBirth.GetValueOrDefault().ToString("yyyy/MM/dd");
                employeeUpdate.Address = query.address;
                employeeUpdate.MailAddress = query.mailAddress;
                employeeUpdate.CustomerId = query.customerId;
                employeeUpdate.ManagerId = query.managerId;
                employeeUpdate.PersonalNumber = query.personalNumber;
                employeeUpdate.AccountBankInfo = query.accountBankInfo;
                employeeUpdate.EntryDate = query.entryDate;
                employeeUpdate.LeavingDate = query.leavingDate;
                employeeUpdate.DepentdentFamily = query.depentdentFamily.GetValueOrDefault();
                employeeUpdate.UserName = query.userName;
                employeeUpdate.PassWord = query.passWord;
                employeeUpdate.Description = query.description;
                employeeUpdate.AvatarFilePath = query.avatarFilePath;
            }
            return employeeUpdate;
        }

        // 社員情報編集
        public EmployeeUpdateInput updateEmployee(EmployeeUpdateInput employeeUpdateInput)
        {
            try
            {
                using (sys_employeeEntities db = new sys_employeeEntities())
                {
                    var path = "";
                    var avatarFile = employeeUpdateInput.AvatarFile;
                    if (avatarFile != null && avatarFile.ContentLength > 0)
                    {
                        path = HelperCommon.saveAvatarFile(employeeUpdateInput.AvatarFile, employeeUpdateInput.Id);
                    }

                    var query = (from e in db.employee
                                 where e.employeeId == employeeUpdateInput.Id
                                 select e).First();

                    if(!query.userName.Equals(employeeUpdateInput.UserName))
                    {
                        if (DataBaseCommon.isDuplicateUsername(employeeUpdateInput.UserName, db))
                        {
                            throw new Exception("社員Usernameが存在です");
                        }
                    }

                    employee employeeUpdate = (employee)query;
                    employeeUpdate.employeeId = employeeUpdateInput.Id;
                    employeeUpdate.name = employeeUpdateInput.Name;
                    employeeUpdate.kataName = employeeUpdateInput.KataName;
                    employeeUpdate.authorityId = employeeUpdateInput.AuthorityId;
                    employeeUpdate.telephoneNumber = employeeUpdateInput.TelephoneNumber;
                    employeeUpdate.dateOfBirth = DateTime.Parse(employeeUpdateInput.DateOfBirth);
                    employeeUpdate.address = employeeUpdateInput.Address;
                    employeeUpdate.mailAddress = employeeUpdateInput.MailAddress;
                    employeeUpdate.customerId = employeeUpdateInput.CustomerId;
                    employeeUpdate.managerId = employeeUpdateInput.ManagerId;
                    employeeUpdate.personalNumber = employeeUpdateInput.PersonalNumber;
                    employeeUpdate.accountBankInfo = employeeUpdateInput.AccountBankInfo;
                    employeeUpdate.entryDate = employeeUpdateInput.EntryDate;
                    employeeUpdate.leavingDate = employeeUpdateInput.LeavingDate;
                    employeeUpdate.depentdentFamily = employeeUpdateInput.DepentdentFamily;
                    employeeUpdate.userName = employeeUpdateInput.UserName;
                    if (!employeeUpdate.passWord.Equals(employeeUpdateInput.PassWord))
                    {
                        employeeUpdate.passWord = HelperCommon.hashPassword(employeeUpdateInput.PassWord);
                    }
                    employeeUpdate.description = employeeUpdateInput.Description;
                    if (!path.Equals(""))
                    {
                        employeeUpdate.avatarFilePath = path;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
                return employeeUpdateInput;
        }

        public Dictionary<string, string> getPersonalDocument(string id)
        {
            try
            {
                return HelperCommon.getPersonalDocuments(id);
            }
            catch(Exception e)
            {
                throw e;
            }
            

        }

        public bool addDocument(string id, HttpPostedFileBase documentFile)
        {
            return HelperCommon.addPersonalDocument(documentFile, id);
        }

        public bool deletePersonalDocument(string id , string filename)
        {
            return HelperCommon.deletePeronalDocument(id , filename); 
        }
    }
}