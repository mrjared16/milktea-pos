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
using System.Drawing;
using QuanLiQuanCaPhe.View;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class TaiKhoanViewModel : BaseViewModel
	{
		public bool Isloaded = false;
		public ICommand LoadedWindowCommand { get; set; }
		public ICommand LuuThongTinAdminCommand { get; set; }
		public ICommand DoiThongTinAdminCommand { get; set; }


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
		private BitmapImage _DisplayedImagePath;
		public BitmapImage DisplayedImagePath
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
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() == true)
				{
					Uri fileUri = new Uri(openFileDialog.FileName);
					temp = fileUri.ToString();
					DisplayedImagePath = new BitmapImage(new Uri(openFileDialog.FileName));
				}
			}
			);
			LuuThongTinAdminCommand = new RelayCommand<Window>((p) =>
			{
				NhanVien item = UserService.GetCurrentUser;
				//hinh anh
				if (!DisplayedImagePath.ToString().Equals(LoadImage(item.HINHANH).ToString()))
				{
					return true;
				}
				//ho ten
				if (!HoTen.Equals(item.HOTEN))
					return true;
				//ngay sinh
				DateTime a = item.NGSINH.Value;
				if (!NgaySinh.Equals(a.ToString("dd/MM/yyyy")))
					return true;
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
				else
					return false;

				;
			}, (p) =>
			{
				try
				{
					var nhanvien = UserService.GetCurrentUser;
					nhanvien.PHAI = GioiTinh;
					nhanvien.HINHANH = ImageToByte2(DisplayedImagePath);
					nhanvien.CMND = CMND;
					nhanvien.CHUCVU = ChucVu;
					nhanvien.DIENTHOAI = SDT;
					nhanvien.HOTEN = HoTen;
					nhanvien.NGSINH = DateTime.ParseExact(NgaySinh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
					nhanvien.DIACHI = DiaChi;
					DataProvider.ISCreated.DB.SaveChangesAsync();
					MessageBox.Show("Lưu thành công!!!");

				}
				catch
				{
					MessageBox.Show("Lưu không thành công :(((");
				}
			}
);
			DoiThongTinAdminCommand = new RelayCommand<Window>((p) =>
			{
				return true;
			}, (p) =>
			{
				ResetPassword resetPassword = new ResetPassword();
				resetPassword.ShowDialog();
			});
		}
		public void loadData()
		{

			NhanVien item = UserService.GetCurrentUser;
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
			//hinh anh ca nhan
			DisplayedImagePath = LoadImage(item.HINHANH);
			File.Delete("tumeo.txt");
		}
		private static BitmapImage LoadImage(byte[] imageData)
		{
			if (imageData == null || imageData.Length == 0) return null;
			var image = new BitmapImage();
			using (var mem = new MemoryStream(imageData))
			{
				mem.Position = 0;
				image.BeginInit();
				image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
				image.CacheOption = BitmapCacheOption.OnLoad;
				image.UriSource = null;
				image.StreamSource = mem;
				image.EndInit();
			}
			image.Freeze();
			return image;
		}
		public static byte[] ImageToByte2(BitmapImage img)
		{
			JpegBitmapEncoder encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(img));
			using (MemoryStream ms = new MemoryStream())
			{
				encoder.Save(ms);
				return ms.ToArray();
			}
		}

	}
}
