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
	public class TaiKhoanViewModel: BaseViewModel
	{
		public bool Isloaded = false;
		public ICommand LoadedWindowCommand { get; set; }
		public bool IsLoaded { get; set; }
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
		public ICommand ChonAnhCommand { get; set; }
		String temp;

		public string DisplayedImagePath
		{
			get { return temp; }
			set { temp = value; OnPropertyChanged(); }
		}

		public ImageSource MyPhoto { get; set; }

		public TaiKhoanViewModel()
		{
			loadData();
			ChonAnhCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() == true)
				{
					Uri fileUri = new Uri(openFileDialog.FileName);
					temp = fileUri.ToString();
					DisplayedImagePath = temp;
				}
			}
			);
			
			
		}
		public void loadData()
		{
			using (var fs1 = new FileStream("tumeo.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				byte[] atemp = new byte[100];
				UTF8Encoding encoding = new UTF8Encoding(true);
				int len = 0;
				while(0<(len=fs1.Read(atemp,0,atemp.Length)))
				{
					 tumeo= encoding.GetString(atemp, 0, len);
				}

			}
			var nhanVien = DataProvider.ISCreated.DB.NhanViens.Where(x => x.TAIKHOAN.Equals(tumeo));
			foreach (var item in nhanVien)
			{
				//ho ten
				HoTen = item.HOTEN;
				//
				TaiKhoan = item.TAIKHOAN;
				//ngay sinh
				DateTime a = item.NGSINH.Value;
				NgaySinh = a.ToString("dd/MM/yyyy");
				// so dien thoai
				SDT = item.DIENTHOAI;
				//mat khau
				MatKhau = item.MATKHAU;
				//chuc vu
				ChucVu = item.CHUCVU;
				//CMND
				CMND = item.CMND;
			}
		}

	}
}