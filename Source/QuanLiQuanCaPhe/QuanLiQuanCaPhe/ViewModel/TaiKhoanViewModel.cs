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
using System.Globalization;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class TaiKhoanViewModel: BaseViewModel
	{
		public bool Isloaded = false;
		public ICommand LoadedWindowCommand { get; set; }
		public ICommand LuuThongTinAdminCommand { get; set; }
		
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
		private string _GioiTinh;
		public string GioiTinh
		{
			get => _GioiTinh;
			set
			{
				_GioiTinh = value;
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
		private string _DisplayedImagePath;
		public string DisplayedImagePath
		{
			get { return _DisplayedImagePath; }
			set { _DisplayedImagePath = value; OnPropertyChanged(); }
		}

		SeviceData seviceData;
		public TaiKhoanViewModel()
		{
			loadData();
			ChonAnhCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
			{
				seviceData = new SeviceData();
				//string temo = DataProvider.ISCreated.DB.NhanViens.First().HOTEN;

				foreach (var item in seviceData.GetCMNDNhanVien("Trương Văn Tú"))
				{
					MessageBox.Show(item.HOTEN);
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() == true)
				{
					Uri fileUri = new Uri(openFileDialog.FileName);
					temp = fileUri.ToString();
					DisplayedImagePath = temp;
					OnPropertyChanged("Avatar");

				}
			}
			);
			LuuThongTinAdminCommand = new RelayCommand<Window>((p) => 
			{
				var nhanVien = DataProvider.ISCreated.DB.NhanViens.Where(x => x.TAIKHOAN.Equals(tumeo));
				foreach (var item in nhanVien)
				{
					//ho ten
					if (!HoTen.Equals(item.HOTEN))
						return true;
					if (!TaiKhoan.ToString().Trim().Equals(tumeo.Trim()))
					{
						return true;
					}
						//ngay sinh
					//	DateTime a = item.NGSINH.Value;
					//if (!NgaySinh.Equals(a.ToString()))
					//	return false;
					//dia chi
					if (!DiaChi.Equals(item.DIACHI))
						return true;
					// so dien thoai

					if (!SDT.Equals(item.DIENTHOAI))
						return true;
					//mat khau
					if (!GioiTinh.Equals(item.PHAI))
						return true;
					//chuc vu
					if (!ChucVu.Equals(item.CHUCVU))
						return true;
					//CMND
					if (!CMND.Equals(item.CMND))
						return true;
				}
				return false;

			; }, (p) =>
			{
				//var nhanvien = DataProvider.ISCreated.DB.NhanViens.Where(x => x.TAIKHOAN == tumeo).SingleOrDefault();
				//nhanvien.TAIKHOAN = TaiKhoan;
				//nhanvien.PHAI = GioiTinh;
				//nhanvien.CMND = CMND;
				//nhanvien.HOTEN = HoTen;
				//nhanvien.NGSINH = DateTime.ParseExact(NgaySinh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				//nhanvien.DIACHI = DiaChi;
				//DataProvider.ISCreated.DB.SaveChangesAsync();
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
				fs1.Close();
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
				//dia chi
				DiaChi = item.DIACHI;
				// so dien thoai
				SDT = item.DIENTHOAI;
				//mat khau
				GioiTinh = item.PHAI;
				//chuc vu
				ChucVu = item.CHUCVU;
				//CMND
				CMND = item.CMND;
			}
			File.Delete("tumeo.txt");
		}

	}
}
