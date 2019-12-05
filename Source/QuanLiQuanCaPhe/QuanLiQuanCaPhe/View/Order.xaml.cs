﻿using System;
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
	/// Interaction logic for Order.xaml
	/// </summary>
	public partial class HomeNhanVien : UserControl
	{

	
		public HomeNhanVien()
		{
			InitializeComponent();
			DataContext = new TaiKhoanNhanVien();
		}

		private void LichLamViecNhanVien(object sender, RoutedEventArgs e)
		{
			DataContext = new LichLamViecNhanVien();
		}

		private void TaiKhoanNhanVien(object sender, RoutedEventArgs e)
		{
			DataContext = new TaiKhoanNhanVien();
		}
	}
}
