using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class MainViewModel:BaseViewModel
	{

		public bool IsLoaded { get; set; }
		public MainViewModel()
		{
			Login login = new Login();
			login.ShowDialog();
		}
	}
}
