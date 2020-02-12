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
using System.Security.Cryptography;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class NhanVienViewModel: BaseViewModel {
        public ICommand findNhanVienCommand { get; set; }
        public ICommand confirmButtonCommmand { get; set; }
        public ICommand cancelButtonCommmand { get; set; }
        public ICommand addNhanVienCommand { get; set; }
        
        public ICommand ChonAnhChiTietNhanVienCommand { get; set; }
        BitmapImage temp;

        private bool isAddActivity = true;

        private string _queryString = "";

        public string queryString
        {
            get => _queryString;
            set
            {
                _queryString = value;
                OnPropertyChanged();
            }
        }

        private string _cancelButtonName="HỦY";

        public string cancelButtonName {
            get => _cancelButtonName;
            set
            {
                _cancelButtonName = value;
                OnPropertyChanged();
            }
        }

        private string _confirmButtonName = "THÊM";

        public string confirmButtonName
        {
            get => _confirmButtonName;
            set
            {
                _confirmButtonName = value;
                OnPropertyChanged();
            }
        }

        private BindingList<NhanVien> _listNhanVien;
      
        public BindingList<NhanVien> listNhanVien
        {
            get
            {
                return _listNhanVien;
            }
            set
            {
                _listNhanVien = value;
                OnPropertyChanged();
            }
        }

		private BitmapImage _HinhAnhNhanVien;

		public BitmapImage HinhAnhNhanVien
		{
            get { return _HinhAnhNhanVien; }
            set { _HinhAnhNhanVien = value; OnPropertyChanged(); }
        }

        public ImageSource MyPhoto { get; set; }

        #region Binding
        private NhanVien _ChiTietNhanVien;
        
        public NhanVien ChiTietNhanVien
        {
            get
            {
                return _ChiTietNhanVien;
            }
            set
            {
                _ChiTietNhanVien = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //itemlistView click
        private NhanVien item { get; set; }
        public NhanVien selectItem
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
            ChiTietNhanVien = new NhanVien();
            if (selectItem != null)
            {
                ChiTietNhanVien.MANV = selectItem.MANV;
                ChiTietNhanVien.HOTEN = selectItem.HOTEN;
                ChiTietNhanVien.LUONG = selectItem.LUONG;
				ChiTietNhanVien.DIACHI = selectItem.DIACHI;
                ChiTietNhanVien.NGSINH = selectItem.NGSINH;
                ChiTietNhanVien.PHAI = selectItem.PHAI;
                ChiTietNhanVien.CMND = selectItem.CMND;
                ChiTietNhanVien.DIENTHOAI = selectItem.DIENTHOAI;
                ChiTietNhanVien.CHUCVU = selectItem.CHUCVU;
                ChiTietNhanVien.TAIKHOAN = selectItem.TAIKHOAN;
				HinhAnhNhanVien = SeviceData.LoadImage(selectItem.HINHANH);
                isAddActivity = false;
                cancelButtonName = "XÓA";
                confirmButtonName = "LƯU";
            }
            else
            {
                isAddActivity = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
            }           
            // DisplayedImagePath = selectItem.HINHANH;
        }

        public NhanVienViewModel()
        {
            ChiTietNhanVien = new NhanVien();

            SeviceData seviceData = new SeviceData();

			ChonAnhChiTietNhanVienCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
					// temp = fileUri.ToString();
					HinhAnhNhanVien = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
            );

            //click vao them mon
            addNhanVienCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddActivity = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
                ChiTietNhanVien = new NhanVien();
                /*NhanVien nhanvien = ChiTietNhanVien;
                if (seviceData.themNhanVien(nhanvien))
                {
                    ChiTietNhanVien = new NhanVien();
                    listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
                }
                else
                {
                    MessageBox.Show("Ma nhan vien da ton tai :((");
                }*/
            });

            //click vao nut cancel
            cancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
             {
                if (isAddActivity)
                     ChiTietNhanVien = new NhanVien();
                else
                 {
                     if (seviceData.xoaNhanVien(ChiTietNhanVien.MANV))
                     { 
                         listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
                         ChiTietNhanVien = new NhanVien();
                         isAddActivity = true;
                         cancelButtonName = "HỦY";
                         confirmButtonName = "THÊM";
                     }
                     else
                         MessageBox.Show("Khong xoa duoc nhan vien nay :((");
                 }
             });

            //click vao nut confirm
            confirmButtonCommmand = new RelayCommand<Object>((p) => {

				if (string.IsNullOrEmpty(ChiTietNhanVien.HOTEN) ||
				string.IsNullOrEmpty(ChiTietNhanVien.NGSINH.Value.ToString("dd/mm/yyyy")) ||
				string.IsNullOrEmpty(ChiTietNhanVien.DIACHI) ||
				string.IsNullOrEmpty(ChiTietNhanVien.DIENTHOAI) ||
				string.IsNullOrEmpty(ChiTietNhanVien.CHUCVU) ||
				string.IsNullOrEmpty(ChiTietNhanVien.CMND) ||
				string.IsNullOrEmpty(ChiTietNhanVien.HINHANH.ToString()) ||
				string.IsNullOrEmpty(ChiTietNhanVien.MANV.ToString()) ||
				string.IsNullOrEmpty(ChiTietNhanVien.LUONG.ToString()))
					return false;
				return true;
			}, (p) =>
            {
                if (isAddActivity)
                {
                    NhanVien nhanvien = ChiTietNhanVien;
                    try
                    {
                        nhanvien.HINHANH = SeviceData.ImageToByte2(HinhAnhNhanVien);
                    }
                    catch
                    {

                    }

                    nhanvien.MATKHAU = MD5Hash(Base64Encode(nhanvien.CMND));

                    if (seviceData.themNhanVien(nhanvien))
                    {
                     
                        MessageBox.Show("Thêm thành công \n Mật khẩu mặc định là CMND!!!");
                        listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
                    }
                    else
                        MessageBox.Show("Ma nhan vien da ton tai :((");
                    ChiTietNhanVien = new NhanVien();
                }
                else
                {
                    NhanVien nhanvien = ChiTietNhanVien;
                    try
                    {
                        nhanvien.HINHANH = SeviceData.ImageToByte2(HinhAnhNhanVien);
                    }
                    catch
                    {

                    }

                    if (seviceData.suaNhanVien(nhanvien))
                    {
                        listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
                        MessageBox.Show("Lưu thành công");
                    } 
                    else
                        MessageBox.Show("Khong chinh sua thong tin nhan vien duoc :((");
                    showDetails();
                }                   
            });

            //click vao nut tim kiem
            findNhanVienCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
              {
				  MessageBox.Show(queryString);

				  listNhanVien = new BindingList<NhanVien>(SeviceData.TimKiemNhanVien(queryString));
              });

            listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


    }
}