//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Staj1_MvcProje.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLKULLANICI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLKULLANICI()
        {
            this.TBLSEPETT = new HashSet<TBLSEPETT>();
            this.TBLIPTALL = new HashSet<TBLIPTALL>();
            this.TBLSATISLAR = new HashSet<TBLSATISLAR>();
        }
    
        public int ID { get; set; }
        public string AD { get; set; }
        public string ŞİFRE { get; set; }
        public string ROL { get; set; }
        public string ISIM { get; set; }
        public string SOYISIM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLSEPETT> TBLSEPETT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLIPTALL> TBLIPTALL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLSATISLAR> TBLSATISLAR { get; set; }
    }
}
