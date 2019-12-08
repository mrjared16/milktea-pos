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
        public double TongDoanhThu
        { get; set; }
        public string colorBtnSP { get; set; }
        public string colorBtnTong { get; set; }

        public ICommand btnSanPhamCommand { get; set; }
        public ICommand btnTongDoanhThuCommand { get; set; }
        public ICommand btnNgayCommand { get; set; }
        public ICommand btnThangCommand { get; set; }
        public ICommand btnNamCommand { get; set; }
        public ICommand btnQuiCommand { get; set; }

        public DoanhThuAdminViewModel()
        {
            TongDoanhThu = 1000000;
            colorBtnTong = colorBtnSP = "#2962FF";
            OnPropertyChanged("colorBtnSP");
            OnPropertyChanged("colorBtnTong");


            /////COmmand
            btnSanPhamCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDoanhThu = new BindingList<DoanhThu>();
                OnPropertyChanged("listDoanhThu");
                loaiDoanhThu = Constants.DOANHTHU_SP;
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
                listDoanhThu = new BindingList<DoanhThu>();
                if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
                {
                    string maLoai = "L001";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    string maLoai = "L001";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
            });

            btnThangCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                txtDoanhThu = "Doanh thu theo tháng";
                OnPropertyChanged("txtDoanhThu");
                listDoanhThu = new BindingList<DoanhThu>();
                if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
                {
                    string maLoai = "L003";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    string maLoai = "L003";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
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
                    string maLoai = "L004";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    string maLoai = "L002";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
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
                    string maLoai = "L001";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
                    OnPropertyChanged("listDoanhThu");
                    OnPropertyChanged("color");
                }
                if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
                {
                    string maLoai = "L003";
                    BindingList<MonAn> items = new BindingList<MonAn>(SeviceData.getListMonAnLoai(maLoai));
                    double SL = 0;
                    double TongTien = 0;
                    foreach (var item in items)
                    {

                        DoanhThu a = new DoanhThu(item, SL, TongTien);
                        listDoanhThu.Add(a);
                    }
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
