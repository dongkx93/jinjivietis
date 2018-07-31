using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Common.AttributeCommon;

namespace WebApplication2.Models.Employee
{
    public class EmployeeAddInput : EmployeeView
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
        
        [Required(ErrorMessage ="社員Idまだ入力しません")]
        [StringLength(6 ,MinimumLength =6 , ErrorMessage = "社員Idが6桁です")]
        [RegularExpression(@"^vis\d{3}$", ErrorMessage ="社員フォーマットがvis***で入力ください")]
        public string Id { get; set; }
        [Required(ErrorMessage ="名前まだ入力しません")]
        public string Name { get; set; }
        public string KataName { get; set; }
        public string AuthorityId { get; set; }
        public string TelephoneNumber { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy/mm/dd}")]
        [Required(ErrorMessage ="生年月日を入力ください")]
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        [EmailAddress(ErrorMessage ="メールアドレスフォーマットが不正です")]
        public string MailAddress { get; set; }
        public string CustomerId { get; set; }
        public string ManagerId { get; set; }
        public string PersonalNumber { get; set; }
        public string AccountBankInfo { get; set; }
        [Required(ErrorMessage ="入社日まだ入力しません")]
        [DisplayFormat(ApplyFormatInEditMode =true , DataFormatString ="{yyyy/mm/dd}")]
        public string EntryDate { get; set; }
        [Range(0 , 99  , ErrorMessage ="扶養人数が数値で入力してください")]
        public int DepentdentFamily { get; set; }
        [Required(ErrorMessage = "UserNameまだ入力しません")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Passwordまだ入力しません")]
        [MinLength(6 , ErrorMessage ="Passwordが6桁以上で入力してください")]
        public string PassWord { get; set; }
        [ImageFileAttribute(ErrorMessage ="イメージファイルがjpegまたpng")]
        public HttpPostedFileBase AvatarFile { get; set; }
        public string Description {get; set;}

        
    }
}