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
    
    public partial class POS_Sessions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POS_Sessions()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public long ID { get; set; }
        public int User_ID { get; set; }
        public System.DateTime Log_At { get; set; }
        public Nullable<System.DateTime> Log_Out { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
