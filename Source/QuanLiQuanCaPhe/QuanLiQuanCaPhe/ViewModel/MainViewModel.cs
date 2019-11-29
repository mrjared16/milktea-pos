using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class MainViewModel:BaseViewModel
	{
		
		//public bool Isloaded = false;
		//public ICommand LoadedWindowCommand { get; set; }
		//public bool IsLoaded { get; set; }
		public MainViewModel()
		{
			//LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
			//	Isloaded = true;
			//	if (p == null)
			//		return;
			//	p.Hide();
			//	Login loginWindow = new Login();
			//	loginWindow.ShowDialog();

			//	if (loginWindow.DataContext == null)
			//		return;
			//	var loginVM = loginWindow.DataContext as LoginViewModel;
			//	if (loginVM.IsLogin)
			//	{
			//		p.Show();
			//	}
			//	else
			//	{
			//		p.Close();
			//	}
			//}
			//  );
		}
	}
}
