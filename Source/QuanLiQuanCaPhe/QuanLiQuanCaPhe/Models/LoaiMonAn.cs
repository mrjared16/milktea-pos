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
    using QuanLiQuanCaPhe.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class LoaiMonAn:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiMonAn()
        {
            this.MonAns = new HashSet<MonAn>();
        }
    
        public string MALOAI { get; set; }

        private string _tenLoai;
        public string TENLOAI
        {
            get
            {
                return _tenLoai;
            }
            set
            {
                _tenLoai = value;
                OnPropertyChanged("TENLOAI");
            }
        }
        //public string TENLOAI { get; set; }
        public Nullable<int> ISDEL { get; set; }
        public Nullable<System.DateTime> CREADTEDAT { get; set; }
        public Nullable<System.DateTime> UPDATEDAT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MonAn> MonAns { get; set; }
    }
}
