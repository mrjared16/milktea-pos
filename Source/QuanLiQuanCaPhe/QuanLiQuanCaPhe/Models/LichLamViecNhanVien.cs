using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
	public class LichLamViecNhanVien:BaseViewModel
	{
		public string Ca1 { get => ca1; set => ca1 = value; }
		public string Ca2 { get => ca2; set => ca2 = value; }
		public string Ca3 { get => ca3; set => ca3 = value; }
		public string Ca4 { get => ca4; set => ca4 = value; }

		private string ca1;
		private string ca2;
		private string ca3;
		private string ca4;

		public LichLamViecNhanVien(string ca1, string ca2, string ca3, string ca4)
		{
			Ca1 = ca1;
			Ca2 = ca2;
			Ca3 = ca3;
			Ca4 = ca4;
		}
	}
}
