//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiQuanCaPhe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CaLamViec
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaLamViec()
        {
            this.LichLamViecs = new HashSet<LichLamViec>();
        }
    
        public string MACALV { get; set; }
        public string TENCA { get; set; }
        public Nullable<System.TimeSpan> GIOBATDAU { get; set; }
        public Nullable<System.TimeSpan> GIOKETTHUC { get; set; }
        public Nullable<int> ISDEL { get; set; }
        public Nullable<System.DateTime> CREADTEDAT { get; set; }
        public Nullable<System.DateTime> UPDATEDAT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichLamViec> LichLamViecs { get; set; }
    }
}
