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
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            this.Invoice_Items = new HashSet<Invoice_Items>();
        }
    
        public long ID { get; set; }
        public string SN { get; set; }
        public long POS_Session_ID { get; set; }
        public System.DateTime Created_At { get; set; }
        public decimal Net_Amount { get; set; }
        public decimal Services_Amount { get; set; }
        public decimal Taxes_Amount { get; set; }
        public decimal Total_Amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice_Items> Invoice_Items { get; set; }
        public virtual POS_Sessions POS_Sessions { get; set; }
    }
}
