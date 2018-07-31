using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Employee
{
    public class EmployeeSearchInput
    {
        private string _id;
        private string _name;
        private string _workSituation;
        private string _customerId;
        private string _authorityId;

        public string Id { get; set; }
        public string Name { get; set; }
        public string WorkSituation { get; set; }
        public string CustomerId { get; set; }
        public string AuthorityId { get; set; }
    }
}