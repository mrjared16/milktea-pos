using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;
using QuanLiQuanCaPhe.Models;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class HomeViewModel: BaseViewModel
	{

		public bool Isloaded = false;
		public ICommand LoadedWindowCommand { get; set; }
		public bool IsLoaded { get; set; }
		public static string tumeo = "";

		private string _UserNameHome;
		public string UserNameHome { get => _UserNameHome; set { _UserNameHome = value; OnPropertyChanged(); } }

		public ICommand ChonAnhCommand { get; set; }
		String temp;

		public string DisplayedImagePath
		{
			get { return temp; }
			set { temp = value; OnPropertyChanged(); }
		}

		public ImageSource MyPhoto { get; set; }

		public HomeViewModel()
		{
			
			ChonAnhCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
			{
				MessageBox.Show(tumeo.ToString());
				//OpenFileDialog openFileDialog = new OpenFileDialog();
				//if (openFileDialog.ShowDialog() == true)
				//{
				//	Uri fileUri = new Uri(openFileDialog.FileName);
				//	temp = fileUri.ToString();
				//	DisplayedImagePath = temp;
				//}
			}
			);
			LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
				Isloaded = true;
				if (p == null)
					return;
				p.Hide();
				Login loginWindow = new Login();
				loginWindow.ShowDialog();
				if (loginWindow.DataContext == null)
					return;
				var loginVM = loginWindow.DataContext as LoginViewModel;
				if (loginVM.IsLogin)
				{
					p.Show();
					tumeo += loginVM.tendangnhap;
					UserNameHome = tumeo;
				}
				else
				{
					p.Close();
				}
			}
  );
		}

	}
}
