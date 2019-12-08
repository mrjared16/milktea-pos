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

namespace QuanLiQuanCaPhe.View

{
	/// <summary>
	/// Interaction logic for MonAn.xaml
	/// </summary>
	public partial class MonAnAdmin : UserControl
	{
		public MonAnAdminViewModel home { get; set; }
		public MonAnAdmin()
		{
			InitializeComponent();
			home = new MonAnAdminViewModel();
			this.DataContext = home;
		}
	}
}
