//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    
    public partial class SP_Employee_Attendance_Sheet_Result
    {
        public long attendance_sheet_Id { get; set; }
        public int employee_shift_Id { get; set; }
        public System.DateTime log_day { get; set; }
        public Nullable<System.TimeSpan> log_In { get; set; }
        public Nullable<int> log_in_applied_shift_rule { get; set; }
        public Nullable<System.TimeSpan> log_out { get; set; }
        public Nullable<int> log_out_applied_shift_rule { get; set; }
        public Nullable<System.TimeSpan> worked_hours { get; set; }
        public System.TimeSpan target_working_hours { get; set; }
        public Nullable<System.TimeSpan> rated_deduction_hours { get; set; }
        public Nullable<System.TimeSpan> rated_overtime { get; set; }
        public Nullable<System.TimeSpan> rated_approved_overtime { get; set; }
        public bool is_absent { get; set; }
        public int monthly_master_ref_Id { get; set; }
        public int shift_Id { get; set; }
        public string shift_title { get; set; }
        public System.TimeSpan shift_checkIn { get; set; }
        public System.TimeSpan shift_checkOut { get; set; }
        public int employee_Id { get; set; }
        public string checkIn_rule_title { get; set; }
        public string checkIn_rule_color { get; set; }
        public string checkOut_rule_title { get; set; }
        public string checkout_rule_color { get; set; }
        public string employee_name { get; set; }
        public Nullable<bool> day_off { get; set; }
    }
}
