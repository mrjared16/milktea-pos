using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
	public class SeviceData
	{
		public SeviceData()
		{
		}

		public List<NhanVien> GetCMNDNhanVien(string HoTen)
		{
			return new List<NhanVien>(DataProvider.ISCreated.DB.NhanViens.Where(x => x.HOTEN == HoTen).ToList());
		}
		public List<NhanVien> GetCMNDNhanVien(string Pass, string abc)
		{
			return new List<NhanVien>(DataProvider.ISCreated.DB.NhanViens.Where(x => x.HOTEN == Pass).ToList());
		}

		public List<MonAn> getListMonAn()
		{
			return new List<MonAn>(DataProvider.ISCreated.DB.MonAns);
		}
		public List<MonAn> getListMonAnLoai(string MaLoai)
		{
			return new List<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x=>x.MALOAI== MaLoai));
		}
		public List<MonAn> getListMonAnTenMon(string TenMon)
		{
			return new List<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == TenMon));
		}
		public List<MonAn> getListMonAnMaMon(string MaMon)
		{
			return new List<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == MaMon));
		}

		public bool tonTaiMonAn(string MaMon )
		{
			var MonAn= DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == MaMon);
			int count = 0;
			foreach (var item in MonAn)
			{
				count++;
			}
			if (count > 0)
				return true;
			else
				return false;
		}

		public bool tonTaiLoaiMonAn(string MaLoai)
		{
			var MonAn = DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.MALOAI == MaLoai);
			int count = 0;
			foreach (var item in MonAn)
			{
				count++;
			}
			if (count > 0)
				return true;
			else
				return false;
		}

		public string themMonAn(MonAn monAn)
		{
			if(!tonTaiLoaiMonAn(monAn.MALOAI))
			{
				return "Mã món ăn không tồn tại";
			}
			if(monAn.TENMON.Equals(""))
			{
				return "Tên món ăn rỗng";
			}
			else
			{
				monAn.CREADTEDAT= DateTime.Now;
				DataProvider.ISCreated.DB.MonAns.Add(monAn);
				DataProvider.ISCreated.DB.SaveChanges();
				return "Thành công";
			}
		}
		public string XoaMonAn(MonAn monAn)
		{
			if (!tonTaiLoaiMonAn(monAn.MALOAI))
			{
				return "Mã món ăn không tồn tại";
			}
			if (monAn.TENMON.Equals(""))
			{
				return "Tên món ăn rỗng";
			}
			else
			{
				var temp = DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == monAn.MAMON).SingleOrDefault();

				temp.ISDEL = 1;
				DataProvider.ISCreated.DB.SaveChanges();
				return "Thành công";
			}

		}

		public string suaMonAn(MonAn monAn)
		{
			if(!tonTaiMonAn(monAn.MAMON))
			{
				return "Món ăn không tồn tại!!!";
			}
			else
			{
				var temp = DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == monAn.MAMON).SingleOrDefault();
				temp.ISDEL = monAn.ISDEL;
				temp.TENMON = monAn.TENMON;
				temp.MAMON = monAn.MAMON;
				temp.HINHANH = monAn.HINHANH;
				temp.MALOAI = monAn.MALOAI;
				temp.GIA = monAn.GIA;
				temp.MOTA = monAn.MOTA;
				temp.TTSP = monAn.TTSP;
				temp.UPDATEDAT = DateTime.Now;
				DataProvider.ISCreated.DB.SaveChanges();
				return "Thành công";
			}
		}

		public List<DoanhThu> DoanhThuTheoLoaiMonHomNay(string maLoai)
		{
			List<DoanhThu> doanhThu = new List<DoanhThu>();
			var isLoaiMonAn = DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.MALOAI == maLoai);
			foreach (var item in isLoaiMonAn)
			{
				return doanhThu;
			}
			

			return doanhThu;
		}

	}
}
