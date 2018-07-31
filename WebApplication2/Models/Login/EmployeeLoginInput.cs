using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Login
{
    public class EmployeeLoginInput
    {
        private string _userName;
        private string _passWord;

        [Required(ErrorMessage ="ユーザネームを入力ください")]
        public string UserName { get ; set; }
        [Required(ErrorMessage = "パスワードを入力ください")]
        public string PassWord { get; set; }
    }
}