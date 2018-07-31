using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Login
{
    public class EmployeeLoginOutput
    {
        private string _id;
        private string _name;
        private string _kataName;
        private string _email;
        private string _telephoneNumber;
        private string _entryDate;
        private string _customerName;
        private string _address;
        private string _accountBankInfo;
        private string _PersonalNunber;
        private string _dateOfBirth;
        private string _authorityId;
        private string _avatarFilePath;

        public string Id { get; set; }
        public string Name { get; set; }
        public string KataName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string EntryDate { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string AccountBankInfo { get; set; }
        public string PersonalNunber { get; set; }
        public string DateOfBirth { get; set; }
        public string AuthorityId { get; set; }
        public string AvatarFilePath { get; set; }
    }
}