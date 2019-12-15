using QuanLiQuanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class DoanhThuAdminViewModel : BaseViewModel
    {
        public string txtDoanhThu { get; set; }
        public BindingList<DoanhThu> listDoanhThu { get; set; }
        public int loaiDoanhThu = -1;//1=>SAN PHAM, 2=> TONG

        private double _tongDoanhThu;
        public double TongDoanhThu
        { get => _tongDoanhThu; set { _tongDoanhThu = value; OnPropertyChanged("TongDoanhThu"); } }
        public string colorBtnSP { get; set; }
        public string colorBtnTong { get; set; }
        private BindingList<LoaiMonAn> _MilkteaCategories;
        public BindingList<LoaiMonAn> MilkteaCategories
        {
            get
            {
                return new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database
                ;
            }
            set
            {
                _MilkteaCategories = value;
                // _MilkteaCategories = new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database
                OnPropertyChanged("MilkteaCategories");
            }
        }
        private LoaiMonAn _selectedLoai { get; set; }
        public LoaiMonAn selectedLoai
        {
            get { return _selectedLoai; }
            set
            {
                if (_selectedLoai != value)
                {
                    _selectedLoai = value;
                    listDoanhThu = new BindingList<DoanhThu>();
                    OnPropertyChanged("listDoanhThu");
                    loaiDoanhThu = Constants.DOANHTHU_SP;//////////////
                    if (loaiDoanhThu == Constants.DOANHTHU_SP)
                    {
                        colorBtnSP = "#1A237E";
                        colorBtnTong = "#2962FF";
                    }
                    else
                    {
                        colorBtnSP = "#2962FF";
                        colorBtnTong = "#1A237E";
                    }
                    OnPropertyChanged("colorBtnSP");
                    OnPropertyChanged("colorBtnTong");

                }
            }
        }

        public ICommand btnSanPhamCommand { get; set; }
        public ICommand btnTongDoanhThuCommand { get; set; }
        public ICommand btnNgayCommand { get; set; }
        public ICommand btnThangCommand { get; set; }
        public ICommand btnNamCommand { get; set; }
        public ICommand btnQuiCommand { get; set; }

        public DoanhThuAdminViewModel()
        {
            MilkteaCategories = new BindingList<LoaiMonAn>();
            MilkteaCategories = new BindingList<LoaiMonAn>(SeviceData.getLoaiMonAn());// get from database
            TongDoanhThu = 0;
            colorBtnTong = colorBtnSP = "#2962FF";
            OnPropertyChanged("colorBtnSP");
            OnPropertyChanged("colorBtnTong");


            /////COmmand
            btnSanPhamCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDoanhThu = new BindingList<DoanhThu>();
                OnPropertyChanged("listDoanhThu");
                loaiDoanhThu = Constants.DOANHTHU_SP;//////////////
                TongDoanhThu = 0;
                if (loaiDoanhThu == Constants.DOANHTHU_SP)
                {
                    colorBtnSP = "#1A237E";
                    colorBtnTong = "#2962FF";
                }
                else
                {
                    colorBtnSP = "#2962FF";
                    colorBtnTong = "#1A237E";
                }
                OnPropertyChanged("colorBtnSP");
                OnPropertyChanged("colorBtnTong");
            });

            btnTongDoanhThuCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDoanhThu = new BindingList<DoanhThu>();
                OnPropertyChanged("listDoanhThu");
                loaiDoanhThu = Constants.DOANHTHU_TONG;
                TongDoanhThu = 0;


                if (loaiDoanhThu == Constants.DOANHTHU_TONG)
                {
                    colorBtnSP = "#2962FF";
                    colorBtnTong = "#1A237E";
                }
                else
                {
                    colorBtnSP = "#1A237E";
                    colorBtnTong = "#2962FF";
                }
                OnPropertyChanged("colorBtnSP");
                OnPropertyChanged("colorBtnTong");
            });


            btnNgayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                txtDoanhThu = "Doanh thu theo ngày";
                OnPropertyChanged("txtDoanhThu");

                if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
                {

                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonHomNay(selectedLoai.MALOAI, 1);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());

                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonHomNay(1, 2);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());
                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
            });

            btnThangCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                txtDoanhThu = "Doanh thu theo tháng";
                OnPropertyChanged("txtDoanhThu");

                if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
                {
                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonThangNay(selectedLoai.MALOAI, 1);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());

                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonThangNay(1, 2);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());

                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
            });

            btnNamCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                txtDoanhThu = "Doanh thu theo năm";
                OnPropertyChanged("txtDoanhThu");
                listDoanhThu = new BindingList<DoanhThu>();
                if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
                {
                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonNamNay(selectedLoai.MALOAI, 1);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());

                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonNamNay(0, 2);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());

                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
            });

            btnQuiCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                txtDoanhThu = "Doanh thu theo quí";
                OnPropertyChanged("txtDoanhThu");
                listDoanhThu = new BindingList<DoanhThu>();
                if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
                {
                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonQuyNay(selectedLoai.MALOAI, 1);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());

                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    listDoanhThu = SeviceData.DoanhThuTheoLoaiMonQuyNay(0, 2);
                    listDoanhThu = new BindingList<DoanhThu>(listDoanhThu.OrderByDescending(x => x.TongTienThu).ToList());

                    TongDoanhThu = SeviceData.tongDoanhThu(listDoanhThu);
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
            });
            ////
        }
        public class Constants
        {
            public static int DOANHTHU_SP = 1;
            public static int DOANHTHU_TONG = 2;

        }
    }
}
