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
    
    public partial class SP_Menu_Parents_Select_Result
    {
        public int ID { get; set; }
        public string Element_Name { get; set; }
        public string Element_Action { get; set; }
        public string Element_Controller { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public int Display_Order { get; set; }
        public System.DateTime Created_At { get; set; }
        public string Created_By { get; set; }
        public int TreeLevel { get; set; }
    }
}