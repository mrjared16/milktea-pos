using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
	public class DoanhThu
	{
		private MonAn _monAn;
		private double _soLuotMua;
		private double _tongTienThu;

		public MonAn monAn { get => _monAn; set => _monAn = value; }
		public double SoLuotMua { get => _soLuotMua; set => _soLuotMua = value; }
		public double TongTienThu { get => _tongTienThu; set => _tongTienThu = value; }

		public DoanhThu(MonAn monAn, double soLuotMua, double tongTienThu)
		{
			this._monAn = monAn;
			_soLuotMua = soLuotMua;
			_tongTienThu = tongTienThu;
		}
	}
}
