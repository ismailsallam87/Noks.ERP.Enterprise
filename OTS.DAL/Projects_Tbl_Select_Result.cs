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
    
    public partial class Projects_Tbl_Select_Result
    {
        public int ID { get; set; }
        public string Project_Name { get; set; }
        public System.DateTime Created_At { get; set; }
        public System.DateTime Project_Start_Date { get; set; }
        public Nullable<System.DateTime> Project_End_Date { get; set; }
        public bool Deleted { get; set; }
        public string Bulk_Tasks { get; set; }
        public string Tasks { get; set; }
    }
}
