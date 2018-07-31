using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Common
{
    public class ConstantCommon
    {
        // システム管理者権限
        public static string ADMIN = "ad001";
        // プロジェクトマナジャー権限
        public static string MANAGER = "mg001";
        // 一般社員権限
        public static string EMPLOYEE = "us001";

        //給料フォルダルートパース
        public static string SALARYFOLDERROOTPATH = HttpContext.Current.Server.MapPath("~/Content/Salary/");

        //給料フォルダパース
        public static string SALARYFOLDERPATH = "\\Content\\Salary\\";

        //社員書類フォルダパース
        public static string EMPLOYEEROOTPATH = HttpContext.Current.Server.MapPath("~/Content/EmployeeInformation/");

        //社員書類フォルダパース
        public static string EMPLOYEEFOLDERPATH = "\\Content\\EmployeeInformation\\";

        //アバターイメージパース
        public static string AVATARPATH = "\\Content\\EmployeeInformation\\10.png";

        //在社
        public const string ZAISHA = "1";

        //退社
        public const string TAISHA = "2";
    }
}