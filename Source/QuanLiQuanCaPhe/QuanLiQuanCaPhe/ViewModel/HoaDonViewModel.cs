using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;
using QuanLiQuanCaPhe.Models;
using System.IO;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class HoaDonViewModel:BaseViewModel
    {
        public ICommand timKiemDonHangHomNayCommand { get; set; }
        public ICommand timKiemDonHangThangNayCommand { get; set; }
        public ICommand timKiemDonHangQuyNayCommand { get; set; }
        public ICommand timKiemDonHangNamNayCommand { get; set; }
        public ICommand findDonHangCommand { get; set; }
        public ICommand xoaHoaDonCommand { get; set; }

        private SeviceData seviceData = new SeviceData();

        private string _queryString="";

        public string queryString
        {
            get => _queryString;
            set
            {
                _queryString = value;
                OnPropertyChanged();
            }
        }

        private BindingList<DonHang> _listDonHang;

        public BindingList<DonHang> listDonHang
        {
            get
            {
                return _listDonHang;
            }
            set
            {
                _listDonHang = value;
                OnPropertyChanged();
            }
        }

        private List<DonHang2> dataSourceDonHang;

        public void SetdataSourceDonHang()
        {
            dataSourceDonHang = new List<DonHang2>();
            foreach (var item in listDonHang)
            {
                DonHang2 temp = new DonHang2();
                temp.MADH = item.MADH;
                temp.MANV = (int)item.MANV;
                temp.CREADTEDAT = item.CREADTEDAT;
                temp.TONGTIEN = item.TONGTIEN;
                temp.TENKH = item.TENKH;
                temp.HOTENNV = seviceData.layTenNhanVien(item.MANV);
                dataSourceDonHang.Add(temp);
            }
            listDonHang2 = new BindingList<DonHang2>(dataSourceDonHang);
        }

        private BindingList<DonHang2> _listDonHang2;

        public BindingList<DonHang2> listDonHang2
        {
            get
            {
                return _listDonHang2;
            }
            set
            {
                _listDonHang2 = value;
                OnPropertyChanged();
            }
        }

        private DonHang2 _donHang;

        public DonHang2 donHang
        {
            get
            {
                return _donHang;
            }
            set
            {
                _donHang = value;
                OnPropertyChanged();
            }
        }

        public class DonHang2 : BaseViewModel
        {
            private int _MADH;
            public int MADH
            {
                get => _MADH; set
                {
                    _MADH = value; OnPropertyChanged();
                }
            }

            private int _MANV;
            public int MANV
            {
                get => _MANV; set
                {
                    _MANV = value; OnPropertyChanged();
                }
            }

            private string _HOTENNV;
            public string HOTENNV
            {
                get => _HOTENNV; set
                {
                    _HOTENNV = value; OnPropertyChanged();
                }
            }


            private Nullable<double> _TONGTIEN;
            public Nullable<double> TONGTIEN
            {
                get => _TONGTIEN; set
                {
                    _TONGTIEN = value; OnPropertyChanged();
                }
            }

            private string _TENKH;
            public string TENKH
            {
                get => _TENKH; set
                {
                    _TENKH = value; OnPropertyChanged();
                }
            }

            private Nullable<int> _ISDEL;
            public Nullable<int> ISDEL
            {
                get => _ISDEL; set
                {
                    _ISDEL = value; OnPropertyChanged();
                }
            }

            private Nullable<System.DateTime> _CREADTEDAT;
            public Nullable<System.DateTime> CREADTEDAT
            {
                get => _CREADTEDAT; set
                {
                    _CREADTEDAT = value; OnPropertyChanged();
                }
            }

            private Nullable<System.DateTime> _UPDATEDAT;
            public Nullable<System.DateTime> UPDATEDAT
            {
                get => _UPDATEDAT; set
                {
                    _UPDATEDAT = value; OnPropertyChanged();
                }
            }
        }

        private List<ChiTietDonhang2> dataSourceChiTietDonHang;

        public void SetDataSourceChiTietDonHang()
        {
            dataSourceChiTietDonHang = new List<ChiTietDonhang2>();
            foreach (var item in listChiTietDonHang)
            {
                ChiTietDonhang2 temp = new ChiTietDonhang2();
                temp.MADH = item.MADH;
                temp.MAMON = item.MAMON;
                temp.SOLUONG = item.SOLUONG;
                temp.DONGIA = item.DONGIA;
                temp.THANHTIEN = item.THANHTIEN;
                temp.GIAMGIA = item.GIAMGIA;
                temp.TENMON = seviceData.layTenMon(item.MAMON);
                dataSourceChiTietDonHang.Add(temp);
            }
            listChiTietDonHang2 = new BindingList<ChiTietDonhang2>(dataSourceChiTietDonHang);
        }

        private BindingList<ChiTietDonhang2> _listChiTietDonHang2;

        public BindingList<ChiTietDonhang2> listChiTietDonHang2
        {
            get
            {
                return _listChiTietDonHang2;
            }
            set
            {
                _listChiTietDonHang2 = value;
                OnPropertyChanged();
            }
        }

        private BindingList<ChiTietDonhang> _listChiTietDonHang;

        public BindingList<ChiTietDonhang> listChiTietDonHang
        {
            get
            {
                return _listChiTietDonHang;
            }
            set
            {
                _listChiTietDonHang = value;
                OnPropertyChanged();
            }
        }

        //itemlistView click
        private DonHang2 item { get; set; }
        public DonHang2 selectItem
        {
            get { return item; }
            set
            {
                if (item != value)
                {
                    item = value;

                    showDetails();
                }
            }
        }

        public void showDetails()
        {
            donHang = new DonHang2();
            if (selectItem != null)
            {
                donHang.MADH = selectItem.MADH;
                donHang.MANV = selectItem.MANV;
                donHang.CREADTEDAT = selectItem.CREADTEDAT;
                donHang.TONGTIEN = selectItem.TONGTIEN;
                donHang.TENKH = selectItem.TENKH;
                donHang.HOTENNV = selectItem.HOTENNV;
                listChiTietDonHang = new BindingList<ChiTietDonhang>(seviceData.danhSachChiTietDonhang(donHang.MADH));
                SetDataSourceChiTietDonHang();
            }
        }

        public class ChiTietDonhang2 : BaseViewModel
        {
            private string _TENMON;
            public string TENMON
            {
                get => _TENMON; set
                {
                    _TENMON = value; OnPropertyChanged();
                }
            }

            private int _MADH;
            public int MADH
            {
                get => _MADH; set
                {
                    _MADH = value; OnPropertyChanged();
                }
            }

            private int _MAMON;
            public int MAMON
            {
                get => _MAMON; set
                {
                    _MAMON = value; OnPropertyChanged();
                }
            }

            private Nullable<double> _SOLUONG;
            public Nullable<double> SOLUONG
            {
                get => _SOLUONG; set
                {
                    _SOLUONG = value; OnPropertyChanged();
                }
            }

            private Nullable<double> _DONGIA;
            public Nullable<double> DONGIA
            {
                get => _DONGIA; set
                {
                    _DONGIA = value; OnPropertyChanged();
                }
            }

            private Nullable<double> _THANHTIEN;
            public Nullable<double> THANHTIEN
            {
                get => _THANHTIEN; set
                {
                    _THANHTIEN = value; OnPropertyChanged();
                }
            }

            private Nullable<double> _GIAMGIA;
            public Nullable<double> GIAMGIA
            {
                get => _GIAMGIA; set
                {
                    _GIAMGIA = value; OnPropertyChanged();
                }
            }

            private Nullable<int> _ISDEL;
            public Nullable<int> ISDEL
            {
                get => _ISDEL; set
                {
                    _ISDEL = value; OnPropertyChanged();
                }
            }

            private Nullable<System.DateTime> _CREADTEDAT;
            public Nullable<System.DateTime> CREADTEDAT
            {
                get => _CREADTEDAT; set
                {
                    _CREADTEDAT = value; OnPropertyChanged();
                }
            }

            private Nullable<System.DateTime> _UPDATEDAT;
            public Nullable<System.DateTime> UPDATEDAT
            {
                get => _UPDATEDAT; set
                {
                    _UPDATEDAT = value; OnPropertyChanged();
                }
            }
        }

        public HoaDonViewModel()
        {
            donHang = new DonHang2();

            //click vao nut xoa don hang
            xoaHoaDonCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (selectItem==null)
                    MessageBox.Show("Ban chua chon don hang de xoa!");
                else if (seviceData.xoaDonHang(donHang.MADH))
                {
                    donHang = new DonHang2();
                    listChiTietDonHang = new BindingList<ChiTietDonhang>(seviceData.danhSachChiTietDonhang(donHang.MADH));
                    SetDataSourceChiTietDonHang();
                }
                else
                    MessageBox.Show("Khong xoa duoc don hang nay :((");
                listDonHang = new BindingList<DonHang>(seviceData.TatCaDonHang());
                SetdataSourceDonHang();
            });

            //tim kiem don hang tu o tim kiem
            findDonHangCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
				//tim kiem voi query string tai day
				listDonHang = new BindingList<DonHang>(SeviceData.TimKiemDonHang(queryString));
                SetdataSourceDonHang();
            });

            //lay danh sach don hang hom nay
            timKiemDonHangHomNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
              {
				  DateTime now = DateTime.Now;
                  if (SeviceData.danhSachDonHangHomNay(now) != null)
                  {
                      listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangHomNay(now));
                      SetdataSourceDonHang();
                  }
                  else
                  {
                      listDonHang = new BindingList<DonHang>();
                      SetdataSourceDonHang();
                  } 
			  });

            //lay danh sach don hang thang nay
            timKiemDonHangThangNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangThangNay(DateTime.Now));
                SetdataSourceDonHang();
            });

            //lay danh sach don hang quy nay
            timKiemDonHangQuyNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangQuyNay(DateTime.Now.Month/3+1));
                SetdataSourceDonHang();
            });

            //lay danh sach don hang nam nay
            timKiemDonHangNamNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangNamNay(DateTime.Now));
                SetdataSourceDonHang();
            });

            listDonHang = new BindingList<DonHang>(seviceData.TatCaDonHang());
            SetdataSourceDonHang();
        }
    }
}