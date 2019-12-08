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
        public TaiKhoanViewModel home { get; set; }
        public MainWindow()
        {

            InitializeComponent();
            home = new TaiKhoanViewModel();
            this.DataContext = home;

        }

        private void taiKhoan(object sender, RoutedEventArgs e)
        {
            home = new TaiKhoanViewModel();
            this.DataContext = home;
        }

        private void MonAnAdmin(object sender, RoutedEventArgs e)
        {
            DataContext = new MonAnAdminViewModel();
        }

        private void DangXuat(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void DoanhThu(object sender, RoutedEventArgs e)
        {
            DataContext = new DoanhThuAdminViewModel();
        }
    }
}
