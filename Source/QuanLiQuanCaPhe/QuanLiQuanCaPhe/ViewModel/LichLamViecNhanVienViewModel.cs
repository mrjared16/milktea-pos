using QuanLiQuanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class LichLamViecNhanVienViewModel:BaseViewModel
	{
		private LichLamViecNhanVien _Thu2;
		public LichLamViecNhanVien Thu2
		{
			get { return _Thu2; } 
			set { _Thu2 = value; OnPropertyChanged("LichLamViecA"); }

		}
		private LichLamViecNhanVien _Thu3;
		public LichLamViecNhanVien Thu3
		{
			get { return _Thu3; }
			set { _Thu3 = value; OnPropertyChanged("LichLamViecA"); }

		}
		private LichLamViecNhanVien _Thu4;
		public LichLamViecNhanVien Thu4
		{
			get { return _Thu4; }
			set { _Thu4 = value; OnPropertyChanged("LichLamViecA"); }

		}
		private LichLamViecNhanVien _Thu5;
		public LichLamViecNhanVien Thu5
		{
			get { return _Thu5; }
			set { _Thu5 = value; OnPropertyChanged("LichLamViecA"); }

		}
		private LichLamViecNhanVien _Thu6;
		public LichLamViecNhanVien Thu6
		{
			get { return _Thu6; }
			set { _Thu6 = value; OnPropertyChanged("LichLamViecA"); }

		}
		private LichLamViecNhanVien _Thu7;
		public LichLamViecNhanVien Thu7
		{
			get { return _Thu7; }
			set { _Thu7 = value; OnPropertyChanged("LichLamViecA"); }

		}
		private LichLamViecNhanVien _ChuNhat;
		public LichLamViecNhanVien ChuNhat
		{
			get { return _ChuNhat; }
			set { _ChuNhat = value; OnPropertyChanged("LichLamViecA"); }

		}

		public LichLamViecNhanVienViewModel()
		{
			Thu2 = new LichLamViecNhanVien("", "di lam", "", "");
			Thu3 = new LichLamViecNhanVien("", "", "di lam", "");
			Thu4 = new LichLamViecNhanVien("", "di lam", "", "");
			Thu5 = new LichLamViecNhanVien("", "di lam", "di lam", "");
			Thu6 = new LichLamViecNhanVien("", "di lam", "", "");
			Thu7 = new LichLamViecNhanVien("", "di lam", "", "di lam");
			ChuNhat = new LichLamViecNhanVien("di lam", "di lam", "", "");
		}

	}
}
