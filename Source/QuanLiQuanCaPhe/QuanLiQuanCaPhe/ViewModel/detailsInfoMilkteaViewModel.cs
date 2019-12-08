using QuanLiQuanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{ 
	public class detailsInfoMilkteaViewModel : BaseViewModel
	{
		public ICommand btnClick_CLick { get; set; }
		public MilkteaInfo id { get; set; }
		public string tenTraSua { get; set; }
		public detailsInfoMilkteaViewModel(MilkteaInfo detailInfoCon)
		{
			btnClick_CLick = new RelayCommand<object>((p) => { return true; }, (p) => {
				MessageBox.Show("knock knock");
			}
			);
			id = new MilkteaInfo();
			//MessageBox.Show(detailInfoCon.tenMon);
			id = detailInfoCon;

			tenTraSua = id.tenMon;
		}
	}
}
