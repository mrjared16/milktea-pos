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

        private DonHang _donHang;

        public DonHang donHang
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
        private DonHang item { get; set; }
        public DonHang selectItem
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
            donHang = new DonHang();
            if (selectItem != null)
            {
                donHang.MADH = selectItem.MADH;
                donHang.MANV = selectItem.MANV;
                donHang.CREADTEDAT = selectItem.CREADTEDAT;
                donHang.TONGTIEN = selectItem.TONGTIEN;
                donHang.TENKH = selectItem.TENKH;
                listChiTietDonHang = new BindingList<ChiTietDonhang>(seviceData.danhSachChiTietDonhang(donHang.MADH));
            }
        }

        public HoaDonViewModel()
        {
            donHang = new DonHang();

            //click vao nut xoa don hang
            xoaHoaDonCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (selectItem==null)
                    MessageBox.Show("Ban chua chon don hang de xoa!");
                else if (seviceData.xoaDonHang(donHang.MADH))
                {
                    donHang = new DonHang();
                    listChiTietDonHang = new BindingList<ChiTietDonhang>(seviceData.danhSachChiTietDonhang(donHang.MADH));
                }
                else
                    MessageBox.Show("Khong xoa duoc don hang nay :((");
                listDonHang = new BindingList<DonHang>(seviceData.TatCaDonHang());
            });

            //tim kiem don hang tu o tim kiem
            findDonHangCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
				//tim kiem voi query string tai day
				listDonHang = new BindingList<DonHang>(SeviceData.TimKiemDonHang(queryString));
			});

            //lay danh sach don hang hom nay
            timKiemDonHangHomNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
              {
				  DateTime now = DateTime.Now;
				  if (SeviceData.danhSachDonHangHomNay(now) != null)
					  listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangHomNay(now));
				  else
					  listDonHang = new BindingList<DonHang>();
			  });

            //lay danh sach don hang thang nay
            timKiemDonHangThangNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangThangNay(DateTime.Now));
				
            });

            //lay danh sach don hang quy nay
            timKiemDonHangQuyNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangQuyNay(DateTime.Now.Month/3+1));
            });

            //lay danh sach don hang nam nay
            timKiemDonHangNamNayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                listDonHang = new BindingList<DonHang>(SeviceData.danhSachDonHangNamNay(DateTime.Now));
            });

            listDonHang = new BindingList<DonHang>(seviceData.TatCaDonHang());
        }
    }
}