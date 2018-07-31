using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WebApplication2.Models.Employee
{
    public class EmployeeSearchOutput
    {
        private string _id;
        private string _name;
        private string _kataName;
        private string _telephoneNumber;
        private string _mailAddress;
        private string _customerName;
        private string _entryDate;
        private string _authority;
        private string _description;

        public string Id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string KataName {
            get
            {
                return _kataName;
            }
            set
            {
                _kataName = value;
            }
        }
        public string TelephoneNumber {
            get
            {
                return _telephoneNumber;
            }
            set
            {
                _telephoneNumber = value;
            }
        }
        public string MailAddress {
            get
            {
                return _mailAddress;
            }
            set
            {
                _mailAddress = value;
            }
        }
        public string CustomerName {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
            }
        }
        public string EntryDate {
            get
            {
                return _entryDate;
            }
            set
            {
                _entryDate = value;
            }
        }
        public string Authority
        {
            get
            {
                return _authority;
            }
            set
            {
                _authority = value;
            }
        }
        public string Description {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
    }
}