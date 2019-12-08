using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class TaiKhoanNhanVienViewModel : BaseViewModel
    {
        public static string tumeo = "";

        private static string _TaiKhoan;
        public string TaiKhoan
        {
            get => _TaiKhoan;
            set
            {
                _TaiKhoan = value;
                OnPropertyChanged();
            }
        }
        private string _MatKhau;
        public string MatKhau
        {
            get => _MatKhau;
            set
            {
                _MatKhau = value;
                OnPropertyChanged();
            }
        }

        private string _NgaySinh;
        public string NgaySinh
        {
            get => _NgaySinh;
            set
            {
                _NgaySinh = value;
                OnPropertyChanged();
            }
        }

        private string _CMND;
        public string CMND
        {
            get => _CMND;
            set
            {
                _CMND = value;
                OnPropertyChanged();
            }
        }

        public string _DiaChi;
        public string DiaChi
        {
            get => _DiaChi;
            set
            {
                _DiaChi = value;
                OnPropertyChanged();
            }
        }

        private string _SDT;
        public string SDT
        {
            get => _SDT;
            set
            {
                _SDT = value;
                OnPropertyChanged();
            }
        }

        private string _ChucVu;
        public string ChucVu
        {
            get => _ChucVu;
            set
            {
                _ChucVu = value;
                OnPropertyChanged();
            }
        }
        private string _HoTen;
        public string HoTen
        {
            get => _HoTen;
            set
            {
                _HoTen = value;
                OnPropertyChanged();
            }
        }
        private string _a;
        public string a
        {
            get { return _a; }
            set
            {
                _a = value;
                OnPropertyChanged("a");
            }
        }
        public ICommand ChonAnhNhanVienCommand { get; set; }
        private string temp;

        public string DisplayedImagePath
        {
            get { return temp; }
            set { temp = value; OnPropertyChanged("DisplayedImagePath"); }
        }
        public TaiKhoanNhanVienViewModel()
        {

            ChonAnhNhanVienCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    temp = fileUri.ToString();
                    a = @"D:\save.png";
                    DisplayedImagePath = temp;
                }
            }
            );
        }


    }
}
