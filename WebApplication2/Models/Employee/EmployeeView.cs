using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Employee
{
    public class EmployeeView
    {
        private string _id;
        private string _name;
        private string _kataName;
        private string _authorityId;
        private string _telephoneNumber;
        private string _dateOfBirth;
        private string _address;
        private string _mailAddress;
        private string _customerId;
        private string _managerId;
        private string _personalNumber;
        private string _accountBankInfo;
        private string _entryDate;
        private int _depentdentFamily;
        private string _userName;
        private string _passWord;
        private HttpPostedFileBase _avatarFile;
        private string _description;

        public string Id { get; set; }
        public string Name { get; set; }
        public string KataName { get; set; }
        public string AuthorityId { get; set; }
        public string TelephoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string MailAddress { get; set; }
        public string CustomerId { get; set; }
        public string ManagerId { get; set; }
        public string PersonalNumber { get; set; }
        public string AccountBankInfo { get; set; }
        public string EntryDate { get; set; }
        public int DepentdentFamily { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public HttpPostedFileBase AvatarFile { get; set; }
        public string Description { get; set; }
    }
}