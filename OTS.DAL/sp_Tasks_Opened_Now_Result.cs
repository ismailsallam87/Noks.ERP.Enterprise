//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OTS.DAL
{
    using System;
    
    public partial class sp_Tasks_Opened_Now_Result
    {
        public long ID { get; set; }
        public string Task_Title { get; set; }
        public string Task_Area { get; set; }
        public string Task_Account_Name { get; set; }
        public System.DateTime Due_Date { get; set; }
        public string Task_Authorized_Phone { get; set; }
        public System.DateTime Expected_End_Time { get; set; }
        public System.DateTime Expected_Start_Time { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string TaskStatus { get; set; }
        public string ProjectName { get; set; }
    }
}
