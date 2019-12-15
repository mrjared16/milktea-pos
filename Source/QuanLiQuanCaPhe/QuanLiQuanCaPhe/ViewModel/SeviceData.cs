using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QuanLiQuanCaPhe.Models
{
	public class SeviceData
	{
		public SeviceData()
		{
		}
		//them vao/////////////////////////////////////////////////////////////////////////////
		public static List<LoaiMonAn> getLoaiMonAn()
		{
			return new List<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1));
		}
		public static List<LoaiMonAn> getLoaiMonAn(int maLoai)
		{
			return new List<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.MALOAI == maLoai && x.ISDEL != 1));
		}

		/// <summary>
		/// Tìm kiếm tên món
		/// </summary>
		/// <param name="TenMon"></param>
		/// <returns></returns>
		public static BindingList<MonAn> getListMonAnTenMon(string searchStr)
		{
			BindingList<MonAn> monAns = new BindingList<MonAn>();
			var temp = new BindingList<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.ISDEL != 1).ToList());
			foreach (var item in temp)
			{
				if (item.TENMON.ToLower().Contains(searchStr.ToLower()))
				{
					if (!monAns.Contains(item))
					{
						monAns.Add(item);
					}
				}
				if (item.GIA.ToString().ToLower().Contains(searchStr.ToLower()))
				{
					if (!monAns.Contains(item))
					{
						monAns.Add(item);
					}
				}
			}
			return monAns;
		}

		public static BindingList<MonAn> getListMonAn()
		{
			BindingList<MonAn> monAns = new BindingList<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.ISDEL != 1).ToList());
			return monAns;
		}
		public static List<MonAn> getListMonAnLoai(int MaLoai)
		{
			List<MonAn> monAns = new List<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MALOAI == MaLoai && x.ISDEL != 1));
			return monAns;
		}
		public static string themMonAn(MonAn monAn)
		{
			if (string.IsNullOrEmpty(monAn.TENMON))
			{
				return "Tên món ăn rỗng";
			}
			if (!tonTaiLoaiMonAn(monAn.MALOAI))
			{
				return "Loại món ăn không tồn tại";
			}
			else
			{
				monAn.CREADTEDAT = DateTime.Now;
				DataProvider.ISCreated.DB.MonAns.Add(monAn);
				DataProvider.ISCreated.DB.SaveChanges();
				return "Thành công";
			}
		}
		public static bool tonTaiMonAn(int MaMon)
		{
			//MessageBox.Show(MaMon);
			var MonAn = DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == MaMon && x.ISDEL != 1);
			int count = 0;
			foreach (var item in MonAn)
			{
				if (item.ISDEL == 0 || item.ISDEL == null)
				{
					count++;
				}
			}
			if (count > 0)
				return true;
			else
				return false;
		}

		public static bool tonTaiLoaiMonAn(int MaLoai)
		{
			var MonAn = DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.MALOAI == MaLoai && x.ISDEL != 1);
			int count = 0;
			foreach (var item in MonAn)
			{
				if (item.ISDEL == 0 || item.ISDEL == null)
				{
					count++;
				}
			}
			if (count > 0)
				return true;
			else
				return false;
		}
		public static string suaMonAn(MonAn monAn)
		{

			MessageBox.Show(monAn.MALOAI.ToString());
			if (!tonTaiLoaiMonAn(monAn.MALOAI))
			{
				return "Mã loại không tồn tại";
			}
			else
			{
				var temp = DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == monAn.MAMON && x.ISDEL != 1).SingleOrDefault();
				if (temp != null)
				{
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
				return "Thất bại";
			}
		}
		public static string XoaMonAn(MonAn monAn)
		{
			if (!tonTaiLoaiMonAn(monAn.MALOAI))
			{
				return "Mã món ăn không tồn tại!!!";
			}
			if (!tonTaiMonAn(monAn.MAMON))
			{
				return "Mã món ăn không tồn tại!!!";
			}
			else
			{
				var temp = DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == monAn.MAMON && x.ISDEL != 1).SingleOrDefault();
				temp.ISDEL = 1;
				DataProvider.ISCreated.DB.SaveChanges();
				return "Thành công";
			}

		}
		/// /////////////////////////////////////////////////////////////////////
		/// 



		public List<MonAn> getListMonAnMaMon(int MaMon)
		{
			List<MonAn> monAns = new List<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MALOAI == MaMon && x.ISDEL != 1));
			return monAns;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="maLoai"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static BindingList<DoanhThu> DoanhThuTheoLoaiMonHomNay(int maLoai, int mode)
		{
			BindingList<DoanhThu> doanhThu = new BindingList<DoanhThu>();
			BindingList<MonAn> MonAn;
			if (mode == 1)
			{
				MonAn = new BindingList<MonAn>(danhSachMonAnTheoLoaiMonAn(maLoai));
			}
			else
			{
				MonAn = getListMonAn();
			}
			string maDon;
			DateTime date = DateTime.Now;
			var ListDonHang = danhSachDonHangHomNay(date);
			if (ListDonHang == null)
				return null;
			foreach (var item in MonAn)
			{
				var CTMonAn = DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MAMON == item.MAMON);
				{
					double SL = 0;
					double TongTien = 0;
					foreach (var item1 in CTMonAn)
					{
						foreach (var item2 in ListDonHang)
						{
							if (item1.MADH == item2.MADH)
							{
								SL += item1.SOLUONG;
								TongTien += item1.THANHTIEN;
							}
						}
					}
					DoanhThu doanh = new DoanhThu(item, SL, TongTien);
					doanhThu.Add(doanh);
				}
			}
			return doanhThu;
		}
		public static double tongDoanhThu(BindingList<DoanhThu> list)
		{
			double tong = 0;
			foreach (var x in list)
			{
				tong += x.TongTienThu;
			}
			return tong;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maLoai"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		// don hang
		public static BindingList<DonHang> danhSachDonHangHomNay(DateTime date)
		{
			BindingList<DonHang> donHangs = new BindingList<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.CREADTEDAT.Value.Day == date.Day && x.CREADTEDAT.Value.Month == date.Month && x.CREADTEDAT.Value.Year == date.Year && x.ISDEL != 1).ToArray());
			int count = 0;
			foreach (var item in donHangs)
			{
				if (item.ISDEL == 0)
				{
					count++;
				}
			}
			if (count > 0)
				return donHangs;
			else
				return null;
		}


		//public BindingList<DoanhThu> DoanhThuTheoLoaiMonTuanNay(string maLoai, int mode)
		//{
		//    BindingList<DoanhThu> doanhThu = new BindingList<DoanhThu>();
		//    BindingList<MonAn> MonAn;
		//    if (mode == 1)
		//    {
		//        MonAn = new BindingList<MonAn>(danhSachMonAnTheoLoaiMonAn(maLoai));
		//    }
		//    else
		//    {
		//        MonAn = getListMonAn();
		//    }
		//    string maDon;
		//    DateTime date = DateTime.Now;
		//    var ListDonHang = danhSachDonHangTuanNay();
		//    foreach (var item in MonAn)
		//    {
		//        var CTMonAn = DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MAMON == item.MAMON);
		//        {
		//            double SL = 0;
		//            double TongTien = 0;
		//            foreach (var item1 in CTMonAn)
		//            {
		//                foreach (var item2 in ListDonHang)
		//                {
		//                    if (item1.MADH == item2.MADH)
		//                    {
		//                        SL += item1.SOLUONG;
		//                        TongTien += item1.THANHTIEN;
		//                    }
		//                }
		//            }
		//            DoanhThu doanh = new DoanhThu(item, SL, TongTien);
		//            doanhThu.Add(doanh);
		//        }
		//    }
		//    return doanhThu;
		//}

		/// <summary>
		/// /////////////////////////////THÁNG 
		/// </summary>
		/// <param name="maLoai"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static BindingList<DoanhThu> DoanhThuTheoLoaiMonThangNay(int maLoai, int mode)
		{
			BindingList<DoanhThu> doanhThu = new BindingList<DoanhThu>();
			BindingList<MonAn> MonAn;
			if (mode == 1)
			{
				MonAn = new BindingList<MonAn>(danhSachMonAnTheoLoaiMonAn(maLoai));
			}
			else
			{
				MonAn = getListMonAn();
			}
			string maDon;
			DateTime date = DateTime.Now;
			var ListDonHang = danhSachDonHangThangNay(date);
			if (ListDonHang == null)
				return null;
			foreach (var item in MonAn)
			{
				var CTMonAn = DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MAMON == item.MAMON);
				{
					double SL = 0;
					double TongTien = 0;
					foreach (var item1 in CTMonAn)
					{
						foreach (var item2 in ListDonHang)
						{
							if (item1.MADH == item2.MADH)
							{
								SL += item1.SOLUONG;
								TongTien += item1.THANHTIEN;
							}
						}
					}
					DoanhThu doanh = new DoanhThu(item, SL, TongTien);
					doanhThu.Add(doanh);
				}
			}
			return doanhThu;
		}

		/// <summary>
		/// /////////////////////////QUÍIIII
		/// </summary>
		/// <param name="maLoai"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static BindingList<DoanhThu> DoanhThuTheoLoaiMonQuyNay(int maLoai, int mode)
		{
			BindingList<DoanhThu> doanhThu = new BindingList<DoanhThu>();
			BindingList<MonAn> MonAn;
			if (mode == 1)
			{
				MonAn = new BindingList<MonAn>(danhSachMonAnTheoLoaiMonAn(maLoai));
			}
			else
			{
				MonAn = getListMonAn();
			}
			string maDon;
			DateTime date = DateTime.Now;
			var ListDonHang = danhSachDonHangQuyNay(GetQuarter(date));
			if (ListDonHang == null)
				return null;
			foreach (var item in MonAn)
			{
				var CTMonAn = DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MAMON == item.MAMON);
				{
					double SL = 0;
					double TongTien = 0;
					foreach (var item1 in CTMonAn)
					{
						foreach (var item2 in ListDonHang)
						{
							if (item1.MADH == item2.MADH)
							{
								SL += item1.SOLUONG;
								TongTien += item1.THANHTIEN;
							}
						}
					}
					DoanhThu doanh = new DoanhThu(item, SL, TongTien);
					doanhThu.Add(doanh);
				}
			}
			return doanhThu;
		}

		/// <summary>
		/// //////////////////////////////NĂM NÀYYYYYYYYYYYYY
		/// </summary>
		/// <param name="maLoai"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static BindingList<DoanhThu> DoanhThuTheoLoaiMonNamNay(int maLoai, int mode)
		{
			BindingList<DoanhThu> doanhThu = new BindingList<DoanhThu>();
			BindingList<MonAn> MonAn;
			if (mode == 1)
			{
				MonAn = new BindingList<MonAn>(danhSachMonAnTheoLoaiMonAn(maLoai));
			}
			else
			{
				MonAn = getListMonAn();
			}
			string maDon;
			DateTime date = DateTime.Now;
			var ListDonHang = danhSachDonHangNamNay(date);
			if (ListDonHang == null)
				return null;
			foreach (var item in MonAn)
			{
				var CTMonAn = DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MAMON == item.MAMON);
				{
					double SL = 0;
					double TongTien = 0;
					foreach (var item1 in CTMonAn)
					{
						foreach (var item2 in ListDonHang)
						{
							if (item1.MADH == item2.MADH)
							{
								SL += item1.SOLUONG;
								TongTien += item1.THANHTIEN;
							}
						}
					}
					DoanhThu doanh = new DoanhThu(item, SL, TongTien);
					doanhThu.Add(doanh);
				}
			}
			return doanhThu;
		}
		public static BindingList<MonAn> danhSachMonAnTheoLoaiMonAn(int maLoai)
		{

			if (!tonTaiLoaiMonAn(maLoai))
			{
				return null;
			}
			return new BindingList<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MALOAI == maLoai).ToList());
		}
		public static List<DonHang> danhSachDonHangTuanNay()
		{
			List<DonHang> temp = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x=>x.ISDEL!=1));
			List<DonHang> donHangs = new List<DonHang>();
			foreach (var item in temp)
			{
				if (GetIso8601WeekOfYear(item.CREADTEDAT.Value) == GetIso8601WeekOfYear(DateTime.Now))
				{
					donHangs.Add(item);
				}
			}

			return donHangs;
		}

		public static int GetIso8601WeekOfYear(DateTime time)
		{
			// Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
			// be the same week# as whatever Thursday, Friday or Saturday are,
			// and we always get those right
			DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
			if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
			{
				time = time.AddDays(3);
			}

			// Return the week of our adjusted day
			return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		}

		public static List<DonHang> danhSachDonHangThangNay(DateTime date)
		{
			List<DonHang> donHangs = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.CREADTEDAT.Value.Month == date.Month && x.CREADTEDAT.Value.Year == date.Year && x.ISDEL != 1));
			return donHangs;
		}

		public static List<DonHang> danhSachDonHangQuyNay(int quy)
		{
			List<DonHang> temp = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.ISDEL != 1));
			List<DonHang> donHangs = new List<DonHang>();
			foreach (var item in temp)
			{
				if (GetQuarter(item.CREADTEDAT.Value) != GetQuarter(DateTime.Now))
				{
					donHangs.Remove(item);
				}
			}
			return donHangs;
		}
		public static int GetQuarter(DateTime date)
		{
			if (date.Month >= 4 && date.Month <= 6)
				return 1;
			else if (date.Month >= 7 && date.Month <= 9)
				return 2;
			else if (date.Month >= 10 && date.Month <= 12)
				return 3;
			else
				return 4;
		}
		public static List<DonHang> danhSachDonHangNamNay(DateTime date)
		{
			List<DonHang> donHangs = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.CREADTEDAT.Value.Year == date.Year&&x.ISDEL!=1));
			int count = 0;
			foreach (var item in donHangs)
			{
				if (item.ISDEL == 0)
				{
					count++;
				}
			}
			if (count > 0)
				return donHangs;
			else
				return null;
		}
		public List<DonHang> TatCaDonHang()
		{
			return new List<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.ISDEL != 1));
		}

		///////////////////////////////////NHAN VIEN
		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <returns></returns>
		public List<NhanVien> danhsachNhanVien()
		{
			List<NhanVien> nhanViens = new List<NhanVien>(DataProvider.ISCreated.DB.NhanViens.Where(x=>x.ISDEL!=1));
			return nhanViens;
		}
		public bool tontaiNhanVien(int maNV)
		{
			var MonAn = DataProvider.ISCreated.DB.NhanViens.Where(x => x.MANV == maNV&&x.ISDEL!=1);
			int count = 0;
			foreach (var item in MonAn)
			{
				if (item.ISDEL == 0)
				{
					count++;
				}
			}
			if (count > 0)
				return true;
			else
				return false;
		}
		public bool themNhanVien(NhanVien nhanvien)
		{
			if (!tontaiNhanVien(nhanvien.MANV))
			{
				return false;
			}
			else
			{
				nhanvien.CREADTEDAT = DateTime.Now;
				DataProvider.ISCreated.DB.NhanViens.Add(nhanvien);
				DataProvider.ISCreated.DB.SaveChanges();
				return true;
			}
		}
		public bool suaNhanVien(NhanVien nhanVien)
		{
			if (!tontaiNhanVien(nhanVien.MANV))
			{
				return false;
			}
			else
			{
				var temp = DataProvider.ISCreated.DB.NhanViens.Where(x => x.MANV == nhanVien.MANV).SingleOrDefault();
				if (temp.ISDEL == 1)
				{
					return false;
				}
				else
				{
					temp.HOTEN = nhanVien.HOTEN;
					temp.DIACHI = nhanVien.DIACHI;
					temp.LUONG = nhanVien.LUONG;
					temp.NGSINH = nhanVien.NGSINH;
					temp.PHAI = nhanVien.PHAI;
					temp.HINHANH = nhanVien.HINHANH;
					temp.CHUCVU = nhanVien.CHUCVU;
					temp.CMND = nhanVien.CMND;
					temp.DIENTHOAI = nhanVien.DIENTHOAI;
					temp.UPDATEDAT = DateTime.Now;
					DataProvider.ISCreated.DB.SaveChanges();
					return true;
				}
			}
		}
		public bool xoaNhanVien(int maNV)
		{
			if (!tontaiNhanVien(maNV))
			{
				return false;
			}
			else
			{

				var temp = DataProvider.ISCreated.DB.NhanViens.Where(x => x.MANV == maNV).SingleOrDefault();
				temp.ISDEL = 1;
				DataProvider.ISCreated.DB.SaveChanges();
				return true;
			}
		}

		public List<ChiTietDonhang> danhSachChiTietDonhang(int maDH)
		{
			List<ChiTietDonhang> chiTietDonhangs = new List<ChiTietDonhang>(DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MADH == maDH && x.ISDEL != 1));
			return chiTietDonhangs;
		}
		public bool themDonHang(DonHang donHang)
		{
			donHang.CREADTEDAT = DateTime.Now;
			DataProvider.ISCreated.DB.DonHangs.Add(donHang);
			DataProvider.ISCreated.DB.SaveChanges();
			return true;
		}
		public bool themChiTietDonHang(ChiTietDonhang CTDonHang)
		{
			if (!tonTaiDonHang(CTDonHang.MADH))
			{
				return false;
			}
			else
			{
				CTDonHang.CREADTEDAT = DateTime.Now;
				DataProvider.ISCreated.DB.ChiTietDonhangs.Add(CTDonHang);
				DataProvider.ISCreated.DB.SaveChanges();
				return true;
			}
		}
		public bool tonTaiDonHang(int MaDH)
		{
			var MonAn = DataProvider.ISCreated.DB.DonHangs.Where(x => x.MADH == MaDH&&x.ISDEL!=1);
			int count = 0;
			foreach (var item in MonAn)
			{
				if (item.ISDEL == 0)
				{
					count++;
				}
			}
			if (count > 0)
				return true;
			else
				return false;
		}
		public bool xoaDonHang(int maDH)
		{
			if (!tonTaiDonHang(maDH))
				return false;
			else
			{
				// xoa don hang
				var temp = DataProvider.ISCreated.DB.DonHangs.Where(x => x.MADH == maDH).SingleOrDefault();
				temp.ISDEL = 1;
				DataProvider.ISCreated.DB.SaveChanges();
				// hoa chi tiet cua don hang do
				var tempCT = DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MADH == maDH);
				foreach (var item in tempCT)
				{
					item.ISDEL = 1;
				}
				DataProvider.ISCreated.DB.SaveChanges();
				return true;
			}
		}
		public List<LoaiMonAn> danhSachLoaiMonAn()
		{
			List<LoaiMonAn> loaiMonAns = new List<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1));
			return loaiMonAns;
		}

		public List<LoaiMonAn> danhSachLoaiMonAnTheoTen(string tenLoaiMonAn)
		{
			List<LoaiMonAn> loaiMonAns = new List<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.TENLOAI == tenLoaiMonAn&&x.ISDEL!=1));
			return loaiMonAns;
		}
		public bool themLoaiMonAn(LoaiMonAn loaiMon)
		{
			if (tonTaiLoaiMonAn(loaiMon.MALOAI))
			{
				return false;
			}
			else
			{
				loaiMon.CREADTEDAT = DateTime.Now;
				DataProvider.ISCreated.DB.LoaiMonAns.Add(loaiMon);
				DataProvider.ISCreated.DB.SaveChanges();
				return true;
			}
		}
		public bool xoaLoaiMonAn(int maLoai)
		{
			if (!tonTaiLoaiMonAn(maLoai))
			{
				return false;
			}
			else
			{
				// xoa don hang
				var temp = DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.MALOAI == maLoai).SingleOrDefault();
				temp.ISDEL = 1;
				DataProvider.ISCreated.DB.SaveChanges();
				// hoa chi tiet cua don hang do
				var tempCT = DataProvider.ISCreated.DB.MonAns.Where(x => x.MALOAI == maLoai);
				foreach (var item in tempCT)
				{
					item.ISDEL = 1;
				}
				DataProvider.ISCreated.DB.SaveChanges();
				return true;
			}
		}
		public bool suaLoaiMonAn(LoaiMonAn loaiMon)
		{
			if (!tonTaiLoaiMonAn(loaiMon.MALOAI))
			{
				return false;
			}
			else
			{
				var temp = DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.MALOAI == loaiMon.MALOAI).SingleOrDefault();
				temp.MALOAI = loaiMon.MALOAI;
				temp.TENLOAI = loaiMon.TENLOAI;
				temp.UPDATEDAT = DateTime.Now;
				DataProvider.ISCreated.DB.SaveChanges();
				return true;
			}
		}
		public static BitmapImage LoadImage(byte[] imageData)
		{
			if (imageData == null || imageData.Length == 0) return null;
			var image = new BitmapImage();
			using (var mem = new MemoryStream(imageData))
			{
				mem.Position = 0;
				image.BeginInit();
				image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
				image.CacheOption = BitmapCacheOption.OnLoad;
				image.UriSource = null;
				image.StreamSource = mem;
				image.EndInit();
			}
			image.Freeze();
			return image;
		}
		public static byte[] ImageToByte2(BitmapImage img)
		{
			JpegBitmapEncoder encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(img));
			using (MemoryStream ms = new MemoryStream())
			{
				encoder.Save(ms);
				return ms.ToArray();
			}
		}

		public static BindingList<DonHang> TimKiemDonHang(string value)
		{
			BindingList<DonHang> donHangs = new BindingList<DonHang>();
			BindingList<DonHang> temp = new BindingList<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x=>x.ISDEL!=1).ToArray());
			foreach (var item in temp)
			{
				if (item.CREADTEDAT.Value.ToString("dd/mm/yyyy").ToLower().Contains(value.ToLower()))
				{
					if (!donHangs.Contains(item))
					{
						donHangs.Add(item);
					}

				}
				if (TimKiemNhanVien(value) != null)
				{
					foreach (var item1 in TimKiemNhanVien(value))
					{
						if (item1.MANV == item.MANV)
						{
							if (!donHangs.Contains(item))
							{
								donHangs.Add(item);
							}
						}
					}
				}
			}
			return donHangs;
		}
		public static BindingList<DonHang> TimKiemDonHang(string value, int MANV)
		{
			BindingList<DonHang> donHangs = new BindingList<DonHang>();
			BindingList<DonHang> temp = new BindingList<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.MANV == MANV&&x.ISDEL!=1).ToArray());
			foreach (var item in temp)
			{
				if (item.CREADTEDAT.Value.ToString("dd/mm/yyyy").ToLower().Contains(value.ToLower()))
				{
					if (!donHangs.Contains(item))
					{
						donHangs.Add(item);
					}

				}
				if (TimKiemNhanVien(value) != null)
				{
					foreach (var item1 in TimKiemNhanVien(value))
					{
						if (item1.MANV == item.MANV)
						{
							if (!donHangs.Contains(item))
							{
								donHangs.Add(item);
							}
						}
					}
				}
			}
			return donHangs;
		}
		public static BindingList<NhanVien> TimKiemNhanVien(string value)
		{
			BindingList<NhanVien> nhanViens = new BindingList<NhanVien>();
			BindingList<NhanVien> temp = new BindingList<NhanVien>(DataProvider.ISCreated.DB.NhanViens.Where(x=>x.ISDEL!=1).ToArray());
			foreach (var item in temp)
			{
				if (item.HOTEN.ToLower().Contains(value.ToLower()))
				{
					if (!nhanViens.Contains(item))
					{
						nhanViens.Add(item);
					}
				}
				else if (item.DIACHI.ToLower().Contains(value.ToLower()))
				{
					if (!nhanViens.Contains(item))
					{
						nhanViens.Add(item);
					}

				}
				else if (item.CMND.ToLower().Contains(value.ToLower()))
				{
					if (!nhanViens.Contains(item))
					{
						nhanViens.Add(item);
					}

				}
				else if (item.NGSINH.Value.ToString("dd/mm/yyyy").ToLower().Contains(value.ToLower()))
				{
					if (!nhanViens.Contains(item))
					{
						nhanViens.Add(item);
					}

				}
				else if (item.DIENTHOAI.ToLower().Contains(value.ToLower()))
				{
					if (!nhanViens.Contains(item))
					{
						nhanViens.Add(item);
					}
				}
				else if (item.MANV.ToString().ToLower().Contains(value.ToLower()))
				{
					if (!nhanViens.Contains(item))
					{
						nhanViens.Add(item);
					}

				}
			}
			if (nhanViens != null)
				return nhanViens;
			return null;
		}
		public static BindingList<LoaiMonAn> TimKiemLoaiMonAn(string value)
		{
			BindingList<LoaiMonAn> loaiMonAns = new BindingList<LoaiMonAn>();
			BindingList<LoaiMonAn> temp = new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x=>x.ISDEL!=1).ToArray());

			foreach (var item in temp)
			{
				if (item.MALOAI.ToString().ToLower().Contains(value.ToLower()))
				{
					if (!loaiMonAns.Contains(item))
					{
						loaiMonAns.Add(item);
					}
				}
				if (item.TENLOAI.ToLower().Contains(value.ToLower()))
				{
					if (!loaiMonAns.Contains(item))
					{
						loaiMonAns.Add(item);
					}
				}
			}
			if (loaiMonAns.Count > 0)
				return loaiMonAns;
			return null;
		}
		public static BindingList<MonAn> TimKiemMonAn(string value)
		{
			BindingList<MonAn> monAns = new BindingList<MonAn>();
			BindingList<MonAn> temp = new BindingList<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x=>x.ISDEL!=1).ToArray());

			foreach (var item in temp)
			{
				if (item.TENMON.ToLower().Contains(value.ToLower()))
				{
					if (!monAns.Contains(item))
					{
						monAns.Add(item);
					}
				}
				else if (item.GIA.ToString().ToLower().Contains(value.ToLower()))
				{
					if (!monAns.Contains(item))
					{
						monAns.Add(item);
					}
				}
				else if (TimKiemLoaiMonAn(value) != null)
				{
					foreach (var item1 in TimKiemLoaiMonAn(value))
					{
						if (item.MALOAI == item1.MALOAI)
						{
							if (!monAns.Contains(item))
							{
								monAns.Add(item);
							}
						}
					}
				}
			}
			if (monAns.Count > 0)
				return monAns;
			return null;
		}
	}
}
