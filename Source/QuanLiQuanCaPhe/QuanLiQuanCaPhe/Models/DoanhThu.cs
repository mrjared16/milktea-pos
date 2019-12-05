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
		private int _soLuotMua;
		private float _tongTienThu;

		public MonAn monAn { get => _monAn; set => _monAn = value; }
		public int SoLuotMua { get => _soLuotMua; set => _soLuotMua = value; }
		public float TongTienThu { get => _tongTienThu; set => _tongTienThu = value; }

		public DoanhThu(MonAn monAn, int soLuotMua, float tongTienThu)
		{
			this._monAn = monAn;
			_soLuotMua = soLuotMua;
			_tongTienThu = tongTienThu;
		}
	}
}
