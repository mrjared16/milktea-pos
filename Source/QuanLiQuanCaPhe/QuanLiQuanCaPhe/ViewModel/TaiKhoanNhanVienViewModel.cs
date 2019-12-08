using Microsoft.Win32;
using QuanLiQuanCaPhe.Models;
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
	public class TaiKhoanNhanVienViewModel: NhanVienLayoutViewModelInterface
	{
		public static string tumeo = "";
		
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
			Title = "Nhan vien";
			loadData();
		}

		public void loadData()
		{
			using (var fs1 = new FileStream("tumeo.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				byte[] atemp = new byte[100];
				UTF8Encoding encoding = new UTF8Encoding(true);
				int len = 0;
				while (0 < (len = fs1.Read(atemp, 0, atemp.Length)))
				{
					tumeo = encoding.GetString(atemp, 0, len);
				}
				fs1.Close();
			}
			var nhanVien = DataProvider.ISCreated.DB.NhanViens.Where(x => x.TAIKHOAN.Equals(tumeo));
			foreach (var item in nhanVien)
			{
				//ho ten
				HoTen = item.HOTEN;
				//ngay sinh
				DateTime a = item.NGSINH;
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
				DisplayedImagePath1 = LoadImage(item.HINHANH);
			}
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
