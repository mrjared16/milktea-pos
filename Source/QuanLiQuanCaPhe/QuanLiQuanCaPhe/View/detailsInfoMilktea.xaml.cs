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

namespace QuanLiQuanCaPhe.View
{
	/// <summary>
	/// Interaction logic for detailsInfoMilktea.xaml
	/// </summary>
	public partial class detailsInfoMilktea : UserControl
	{
		public detailsInfoMilkteaViewModel viewmodel { get; set; }
		//public string parameter { get; set; }
		public static readonly DependencyProperty myGridSizeProperty =
	   DependencyProperty.Register("detailInfoCon", typeof(MilkteaInfo), typeof(detailsInfoMilktea), new FrameworkPropertyMetadata(null));

		public MilkteaInfo detailInfoCon
		{
			get { return (MilkteaInfo)GetValue(myGridSizeProperty); }
			set
			{
				SetValue(myGridSizeProperty, value);
			}
		}


		public detailsInfoMilktea()
		{

			InitializeComponent();

			Loaded += (sender, args) =>
			{
				MessageBox.Show("tao cua so moi");
				viewmodel = new detailsInfoMilkteaViewModel(detailInfoCon);//truyen qua cho vm

				this.DataContext = viewmodel;
			};
		}

	}
}
