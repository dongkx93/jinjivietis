//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class salary
    {
        public string salaryId { get; set; }
        public string employeeId { get; set; }
        public string paymentDate { get; set; }
        public string salaryDetailFilePath { get; set; }
        public Nullable<double> totalPaidAmount { get; set; }
        public Nullable<double> deductionAmount { get; set; }
        public Nullable<double> paidAmount { get; set; }
        public Nullable<double> transportationExpenses { get; set; }
        public Nullable<double> housingRental { get; set; }
        public Nullable<double> overTimePayment { get; set; }
        public Nullable<double> lateNightOverTimePayment { get; set; }
        public Nullable<double> lateWorkDeduction { get; set; }
        public Nullable<double> absentDeduction { get; set; }
        public string description { get; set; }
    }
}
