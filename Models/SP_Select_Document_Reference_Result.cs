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
    
    public partial class SP_Select_Document_Reference_Result
    {
        public int Id { get; set; }
        public string document_type_title { get; set; }
        public string icon { get; set; }
        public bool valid { get; set; }
        public int expiration_days { get; set; }
        public bool required { get; set; }
    }
}