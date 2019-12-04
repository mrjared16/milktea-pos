using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.ViewModels;
using QuanLiQuanCaPhe.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiQuanCaPhe.ViewModels
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
