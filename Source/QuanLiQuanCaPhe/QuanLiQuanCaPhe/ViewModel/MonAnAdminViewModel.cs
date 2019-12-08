using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class MonAnAdminViewModel:BaseViewModel
	{
		public ICommand selectedItemListMilk_Clicked { get; set; }
		public ICommand addMilkteaCommand { get; set; }
		public ICommand LoadedMenuUCCommand { get; set; }

		//itemlistView click
		private MilkteaInfo temp { get; set; }
		public MilkteaInfo selectItem_Menu
		{
			get { return temp; }
			set
			{
				if (temp != value)
				{
					temp = value;
					showDetails();
				}
			}
		}

		public void showDetails()
		{
			OnPropertyChanged("milkTeaInfoCha");
			OnPropertyChanged("detailInfoCon");
		}

		// truyen data qua man hinh detail
		public static readonly DependencyProperty SudokuSizeProperty =
		DependencyProperty.Register("milkTeaInfoCha", typeof(MilkteaInfo), typeof(MonAnAdmin), new FrameworkPropertyMetadata(null));

		///// 
		///public event PropertyChangedEventHandler PropertyChanged;



		public List<Category> MilkteaCategories { get; set; }
		public BindingList<MilkteaInfo> _listMilkteaInfo { get; set; }
		public detailsInfoMilktea details;
		public bool clickOnItemMenu = true;

		private bool _a;
		public bool ButtonVisibility
		{
			get { return _a; }
			set
			{
				_a = value;
				OnPropertyChanged("ButtonVisibility");
			}
		}
		public MonAnAdminViewModel()
		{
			/// <summary>
			/// Command
			/// </summary>
			/// 
			selectedItemListMilk_Clicked = new RelayCommand<object>((p) => { return true; }, (p) => {
				MessageBox.Show("knock knock");
			}
		   );



			addMilkteaCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{
				MessageBox.Show("aaa");
			});

			ButtonVisibility = false;
			MilkteaCategories = new List<Category>
			{
				new Category
				{
					Name="Tra sua",

				},
				new Category
				{
					Name="Hong tra",

				}
			};
			//Object la tham so truyen vao khi goi su kien loaded
			LoadedMenuUCCommand = new RelayCommand<UserControl>((p) => { return true; }, (parent) =>
			{
				//int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
				//for (int i = 0; i < childrenCount; i++)
				//{
				//    var child = VisualTreeHelper.GetChild(parent, i);
				//    // If the child is not of the request child type child
				//     if (!string.IsNullOrEmpty("details"))
				//    {
				//        var frameworkElement = child as FrameworkElement;
				//        // If the child's name is set for search
				//        if (frameworkElement != null && frameworkElement.Name == "details")
				//        {
				//            var childFound = child as Grid;
				//            childFound.Visibility = Visibility.Hidden;
				//        }
				//    }

				//}
				//isShowDetails = Visibility.Hidden;
			});





			_listMilkteaInfo = new BindingList<MilkteaInfo>();
			for (int i = 0; i < 10; i++)
			{
				MilkteaInfo a = new MilkteaInfo();
				a.tenMon = "Tra sua tran chau " + i.ToString();
				a.gia = 50000 + i * 5000;
				a.imgUrl = "../images/trasua.jpg";
				_listMilkteaInfo.Add(a);
			}
		}


		public class Category
		{
			public string Name { get; set; }
		}

		public string getImageAbsolutePath(object relativePath)
		{
			string absolutePath =
				$"{AppDomain.CurrentDomain.BaseDirectory}images\\{relativePath}";
			MessageBox.Show(absolutePath);
			return absolutePath;
		}
	}
}
