using Microsoft.Win32;
using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
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
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new TaiKhoanViewModel();
		}

		private void taiKhoan(object sender, RoutedEventArgs e)
		{
			DataContext = new TaiKhoanViewModel();
			TaiKhoan.Background = Brushes.LightGreen;
			DonHang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.ForestGreen;
			LoaiMonAn.Background = Brushes.ForestGreen;
			DoanhThu.Background = Brushes.ForestGreen;
		}

		private void MonAnAdmin(object sender, RoutedEventArgs e)
		{
			DataContext = new MonAnAdminViewModel();
			TaiKhoan.Background = Brushes.ForestGreen;
			DonHang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.LightGreen;
			LoaiMonAn.Background = Brushes.ForestGreen;
			DoanhThu.Background = Brushes.ForestGreen;
		}

		private void DangXuat(object sender, RoutedEventArgs e)
		{
			Login login = new Login();
			login.Show();
			this.Close();
		}

		private void doanhThu(object sender, RoutedEventArgs e)
		{
			DataContext = new DoanhThuViewModel();
			TaiKhoan.Background = Brushes.ForestGreen;
			DonHang.Background = Brushes.ForestGreen;
			MonAn.Background = Brushes.ForestGreen;
			LoaiMonAn.Background = Brushes.ForestGreen;
			DoanhThu.Background = Brushes.LightGreen ;
		}
	}
}
