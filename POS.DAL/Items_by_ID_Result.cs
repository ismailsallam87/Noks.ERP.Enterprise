//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POS.DAL
{
    using System;
    
    public partial class Items_by_ID_Result
    {
        public int ID { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }
        public System.DateTime Created_At { get; set; }
        public decimal Current_Stock { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public decimal Selling_Price { get; set; }
        public decimal Cost { get; set; }
        public long price_list_id { get; set; }
    }
}