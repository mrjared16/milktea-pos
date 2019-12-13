using Microsoft.Win32;
using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class TaiKhoanNhanVienViewModel:BaseViewModel
	{
		public static string tumeo = "";
		public ICommand DoiThongTinNhanVienCommand { get; set; }

		private string _NgaySinhNhanVien;
		public string NgaySinhNhanVien
		{
			get => _NgaySinhNhanVien;
			set
			{
				_NgaySinhNhanVien = value;
				OnPropertyChanged();
			}
		}

		private string _CMNDNhanVien;
		public string CMNDNhanVien
		{
			get => _CMNDNhanVien;
			set
			{
				_CMNDNhanVien = value;
				OnPropertyChanged();
			}
		}

		public string _DiaChiNhanVien;
		public string DiaChiNhanVien
		{
			get => _DiaChiNhanVien;
			set
			{
				_DiaChiNhanVien = value;
				OnPropertyChanged();
			}
		}

		private string _SDTNhanVien;
		public string SDTNhanVien
		{
			get => _SDTNhanVien;
			set
			{
				_SDTNhanVien = value;
				OnPropertyChanged();
			}
		}

		private string _ChucVuNhanVien;
		public string ChucVuNhanVien
		{
			get => _ChucVuNhanVien;
			set
			{
				_ChucVuNhanVien = value;
				OnPropertyChanged();
			}
		}
		private string _HoTenNhanVien;
		public string HoTenNhanVien
		{
			get => _HoTenNhanVien;
			set
			{
				_HoTenNhanVien = value;
				OnPropertyChanged();
			}
		}

		private string _GioiTinhNhanVien;
		public string GioiTinhNhanVien
		{
			get => _GioiTinhNhanVien;
			set
			{
				_GioiTinhNhanVien = value;
				OnPropertyChanged();
			}
		}
		public ICommand ChonAnhNhanVienCommand { get; set; }
		String temp;
		private BitmapImage _DisplayedImagePath1;
		public BitmapImage DisplayedImagePath1
		{
			get { return _DisplayedImagePath1; }
			set { _DisplayedImagePath1 = value; OnPropertyChanged(); }
		}

		public TaiKhoanNhanVienViewModel()
		{
			loadData();
			DoiThongTinNhanVienCommand = new RelayCommand<Window>((p) =>
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
			HoTenNhanVien = item.HOTEN;
			//ngay sinh
			DateTime a = item.NGSINH.Value;
			NgaySinhNhanVien = a.ToString("dd/MM/yyyy");
			//dia chi
			DiaChiNhanVien = item.DIACHI;
			// so dien thoai
			SDTNhanVien = item.DIENTHOAI;
			//mat khau
			GioiTinhNhanVien = item.PHAI;
			//chuc vu
			ChucVuNhanVien = item.CHUCVU;
			//CMND
			CMNDNhanVien = item.CMND;
			//hinh anh ca nhan
			DisplayedImagePath1 = LoadImage(item.HINHANH);
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
