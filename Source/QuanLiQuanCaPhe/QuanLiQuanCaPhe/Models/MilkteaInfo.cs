using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
	public class MilkteaInfo : BaseViewModel
	{
		public string _tenMon;
		public string tenMon
		{
			get { return _tenMon; }
			set
			{
				_tenMon = value;
				OnPropertyChanged("tenMon");
			}
		}


		public string _maMon;
		public string maMon
		{
			get { return _maMon; }
			set
			{
				_maMon = value;
				OnPropertyChanged("maMon");
			}
		}
		public string _maLoai;
		public string maLoai
		{
			get { return _maLoai; }
			set
			{
				_maLoai = value;
				OnPropertyChanged("maLoai");
			}
		}
		public string _moTa;
		public string moTa
		{
			get { return _moTa; }
			set
			{
				_moTa = value;
				OnPropertyChanged("moTa");
			}
		}
		public float _gia;
		public float gia
		{
			get { return _gia; }
			set
			{
				_gia = value;
				OnPropertyChanged("gia");
			}
		}

		public float _SL;
		public float SL
		{
			get { return _SL; }
			set
			{
				_SL = value;
				OnPropertyChanged("tenMon");
			}
		}


		public string _imgUrl;
		public string imgUrl
		{
			get { return _imgUrl; }
			set
			{
				_imgUrl = value;
				OnPropertyChanged("imgUrl");
			}
		}
	}
}
