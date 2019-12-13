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
using System.Windows.Shapes;

namespace QuanLiQuanCaPhe
{
	/// <summary>
	/// Interaction logic for NhanVienMainWindow.xaml
	/// </summary>
	public partial class NhanVienMainWindow : Window
	{
		public NhanVienMainWindow()
		{
			InitializeComponent();
			DataContext = new HomeNhanVienViewModel();
		}
		private void taiKhoan(object sender, RoutedEventArgs e)
		{
			DataContext = new HomeNhanVienViewModel();
		}

		private void MonAnAdmin(object sender, RoutedEventArgs e)
		{
			DataContext = new MonAnAdminViewModel();
		}

		private void BanHang(object sender, RoutedEventArgs e)
		{

		}
		private void LichSuBanHang(object sender, RoutedEventArgs e)
		{

		}

		private void DangXuat(object sender, RoutedEventArgs e)
		{
			Login login = new Login();
			login.Show();
			this.Close();
		}
	}
}
