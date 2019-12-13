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

namespace QuanLiQuanCaPhe.View
{
	/// <summary>
	/// Interaction logic for HomeNhanVien.xaml
	/// </summary>
	public partial class HomeNhanVien : UserControl
	{
		public HomeNhanVien()
		{
			//Title = "Nhan vien";
			InitializeComponent();
			DataContext = new TaiKhoanNhanVien();
			TaiKhoan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C3345"));
			LichLamViec.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF585E6E"));
		}
		private void TaiKhoan_Click(object sender, RoutedEventArgs e)
		{
			DataContext = new TaiKhoanNhanVien();
			TaiKhoan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C3345"));
			LichLamViec.Background= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF585E6E"));
		}

		private void LichLamViec_Click(object sender, RoutedEventArgs e)
		{
			DataContext = new LichLamViecNhanVien();
			LichLamViec.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C3345"));
			TaiKhoan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF585E6E"));
		}
	}
}
