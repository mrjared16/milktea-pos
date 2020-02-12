using Microsoft.Win32;
using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLiQuanCaPhe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string tumeo;
		private BitmapImage _HinhAnhAdmin;
		public BitmapImage HinhAnhAdmin
		{
			get { return _HinhAnhAdmin; }
			set { _HinhAnhAdmin = value; }
		}
		public MainWindow()
		{
			_HinhAnhAdmin = LoadImage(UserService.GetCurrentUser.HINHANH);
			InitializeComponent();
			DataContext = new TaiKhoanViewModel();
			
		}

		private void taiKhoan(object sender, RoutedEventArgs e)
		{
			DataContext = new TaiKhoanViewModel();
			TaiKhoan.Background = Brushes.LightGreen;
			donhang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.ForestGreen;
			loaimonan.Background = Brushes.ForestGreen;
			nhanvien.Background = Brushes.ForestGreen;
			_HinhAnhAdmin = LoadImage(UserService.GetCurrentUser.HINHANH);
			DoanhThu.Background = Brushes.ForestGreen;
		}

		private void MonAnAdmin(object sender, RoutedEventArgs e)
		{
			DataContext = new MonAnAdminViewModel();
			TaiKhoan.Background = Brushes.ForestGreen;
			donhang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.LightGreen;
			loaimonan.Background = Brushes.ForestGreen;
			DoanhThu.Background = Brushes.ForestGreen;
			nhanvien.Background = Brushes.ForestGreen;

			loadData();
			_HinhAnhAdmin = LoadImage(UserService.GetCurrentUser.HINHANH);

		}

		private void DangXuat(object sender, RoutedEventArgs e)
		{
			Login login = new Login();
			login.Show();
			this.Close();
		}
		private void doanhThu(object sender, RoutedEventArgs e)
		{
			DataContext = new DoanhThuAdminViewModel();
			TaiKhoan.Background = Brushes.ForestGreen;
			donhang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.ForestGreen;
			loaimonan.Background = Brushes.ForestGreen;
			DoanhThu.Background = Brushes.LightGreen ;
			nhanvien.Background = Brushes.ForestGreen;

			loadData();
			_HinhAnhAdmin = LoadImage(UserService.GetCurrentUser.HINHANH);

		}

		private void NhanVien(object sender, RoutedEventArgs e)
		{
			DataContext = new NhanVienViewModel();
			TaiKhoan.Background = Brushes.ForestGreen;
			donhang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.ForestGreen;
			loaimonan.Background = Brushes.ForestGreen;
			DoanhThu.Background = Brushes.ForestGreen;
			nhanvien.Background = Brushes.LightGreen;
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
				//hinh anh ca nhan
				_HinhAnhAdmin = LoadImage(item.HINHANH);
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

		private void DonHang(object sender, RoutedEventArgs e)
		{
			DataContext = new HoaDonViewModel();
			TaiKhoan.Background = Brushes.ForestGreen;
			donhang.Background = Brushes.LightGreen;
			MonAn.Background = Brushes.ForestGreen;
			loaimonan.Background = Brushes.ForestGreen;
			DoanhThu.Background = Brushes.ForestGreen;
			nhanvien.Background = Brushes.ForestGreen;
		}

		private void LoaiMonAn(object sender, RoutedEventArgs e)
		{
			DataContext = new LoaiMonAnViewModel();
			TaiKhoan.Background = Brushes.ForestGreen;
			donhang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.ForestGreen;
			loaimonan.Background = Brushes.LightGreen;
			DoanhThu.Background = Brushes.ForestGreen;
			nhanvien.Background = Brushes.ForestGreen;
		}
	}
}
