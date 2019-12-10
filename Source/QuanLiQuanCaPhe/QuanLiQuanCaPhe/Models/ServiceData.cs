using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            return new List<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns);
        }

        /// <summary>
        /// Tìm kiếm tên món
        /// </summary>
        /// <param name="TenMon"></param>
        /// <returns></returns>
        public static BindingList<MonAn> getListMonAnTenMon(string searchStr)
        {
            BindingList<MonAn> monAns = new BindingList<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.TENMON.Contains(searchStr) && x.ISDEL != 1).ToList());
            foreach (var item in monAns)
            {
                if (item.ISDEL == 1)
                {
                    monAns.Remove(item);
                }
            }
            return monAns;
        }

        public static BindingList<MonAn> getListMonAn()
        {
            BindingList<MonAn> monAns = new BindingList<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.ISDEL != 1).ToList());
            return monAns;
        }
        public static List<MonAn> getListMonAnLoai(string MaLoai)
        {
            List<MonAn> monAns = new List<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MALOAI == MaLoai && x.ISDEL != 1));
            foreach (var item in monAns)
            {
                if (item.ISDEL == 1)
                {
                    monAns.Remove(item);
                }
            }
            return monAns;
        }
        public static string themMonAn(MonAn monAn)
        {
            if (!tonTaiLoaiMonAn(monAn.MALOAI))
            {
                return "Mã loại không tồn tại";
            }
            if (string.IsNullOrEmpty(monAn.TENMON))
            {
                return "Tên món ăn rỗng";
            }
            if (tonTaiMonAn(monAn.MAMON))
            {
                return "Mã món đã tồn tại";
            }
            else
            {
                monAn.CREADTEDAT = DateTime.Now;
                DataProvider.ISCreated.DB.MonAns.Add(monAn);
                DataProvider.ISCreated.DB.SaveChanges();
                return "Thành công";
            }
        }
        public static bool tonTaiMonAn(string MaMon)
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

        public static bool tonTaiLoaiMonAn(string MaLoai)
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
            if (!tonTaiMonAn(monAn.MAMON))
            {
                return "Món ăn không tồn tại!!!";
            }
            if (!tonTaiLoaiMonAn(monAn.MALOAI))
            {
                return "Mã loại không tồn tại";
            }
            else
            {
                var temp = DataProvider.ISCreated.DB.MonAns.Where(x => x.MAMON == monAn.MAMON && x.ISDEL != 1).SingleOrDefault();
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
            if (string.IsNullOrEmpty(monAn.TENMON))
            {
                return "Tên món ăn rỗng!!!";
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



        public List<MonAn> getListMonAnMaMon(string MaMon)
        {
            List<MonAn> monAns = new List<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MALOAI == MaMon));
            foreach (var item in monAns)
            {
                if (item.ISDEL == 1)
                {
                    monAns.Remove(item);
                }
            }
            return monAns;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="maLoai"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static BindingList<DoanhThu> DoanhThuTheoLoaiMonHomNay(string maLoai, int mode)
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
            BindingList<DonHang> donHangs = new BindingList<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.CREADTEDAT.Value.Day == date.Day).ToArray());
            int count = 0;
            foreach (var item in donHangs)
            {
                if (item.ISDEL == 0)
                {
                    count++;
                }
            }
            //if (count > 0)
            return donHangs;
            //else
            //	return null;
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
        public static BindingList<DoanhThu> DoanhThuTheoLoaiMonThangNay(string maLoai, int mode)
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
        public static BindingList<DoanhThu> DoanhThuTheoLoaiMonQuyNay(string maLoai, int mode)
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
        public static BindingList<DoanhThu> DoanhThuTheoLoaiMonNamNay(string maLoai, int mode)
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
        public static BindingList<MonAn> danhSachMonAnTheoLoaiMonAn(string maLoai)
        {

            if (!tonTaiLoaiMonAn(maLoai))
            {
                return null;
            }
            return new BindingList<MonAn>(DataProvider.ISCreated.DB.MonAns.Where(x => x.MALOAI == maLoai).ToList());
        }
        public static List<DonHang> danhSachDonHangTuanNay()
        {
            List<DonHang> donHangs = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs);
            foreach (var item in donHangs)
            {
                if (GetIso8601WeekOfYear(item.CREADTEDAT.Value) != GetIso8601WeekOfYear(DateTime.Now))
                {
                    donHangs.Remove(item);
                }
            }
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
            List<DonHang> donHangs = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.CREADTEDAT.Value.Month == date.Month));
            int count = 0;
            foreach (var item in donHangs)
            {
                if (item.ISDEL == 0)
                {
                    count++;
                }
            }
            //if (count > 0)
            return donHangs;
            //else
            //	return null;
        }

        public static List<DonHang> danhSachDonHangQuyNay(int quy)
        {
            List<DonHang> donHangs = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs);
            foreach (var item in donHangs)
            {
                if (GetQuarter(item.CREADTEDAT.Value) != GetQuarter(DateTime.Now))
                {
                    donHangs.Remove(item);
                }
            }
            int count = 0;
            foreach (var item in donHangs)
            {
                if (item.ISDEL == 0)
                {
                    count++;
                }
            }
            //if (count > 0)
            return donHangs;
            //else
            //	return null;
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
            List<DonHang> donHangs = new List<DonHang>(DataProvider.ISCreated.DB.DonHangs.Where(x => x.CREADTEDAT.Value.Year == date.Year));
            int count = 0;
            foreach (var item in donHangs)
            {
                if (item.ISDEL == 0)
                {
                    count++;
                }
            }
            //if (count > 0)
            return donHangs;
            //else
            //	return null;
        }


        ///////////////////////////////////NHAN VIEN
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public List<NhanVien> danhsachNhanVien()
        {
            List<NhanVien> nhanViens = new List<NhanVien>(DataProvider.ISCreated.DB.NhanViens);
            foreach (var item in nhanViens)
            {
                if (item.ISDEL == 1)
                {
                    nhanViens.Remove(item);
                }
            }
            return nhanViens;
        }
        public List<NhanVien> timKiemNhanVien(string value)
        {
            List<NhanVien> nhanViens = new List<NhanVien>();
            return nhanViens;
        }
        public bool tontaiNhanVien(string maNV)
        {
            var MonAn = DataProvider.ISCreated.DB.NhanViens.Where(x => x.MANV == maNV);
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
                    temp.CHUCVU = nhanVien.CHUCVU;
                    temp.CMND = nhanVien.CMND;
                    temp.DIENTHOAI = nhanVien.DIENTHOAI;
                    temp.UPDATEDAT = DateTime.Now;
                    DataProvider.ISCreated.DB.SaveChanges();
                    return true;
                }
            }
        }
        public bool xoaNhanVien(string maNV)
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




        public List<ChiTietDonhang> danhSachChiTietDonhang(string maDH)
        {
            List<ChiTietDonhang> chiTietDonhangs = new List<ChiTietDonhang>(DataProvider.ISCreated.DB.ChiTietDonhangs.Where(x => x.MADH == maDH));
            int count = 0;
            foreach (var item in chiTietDonhangs)
            {
                if (item.ISDEL == 0)
                {
                    count++;
                }
            }
            if (count > 0)
                return chiTietDonhangs;
            else
                return null;
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
        public bool tonTaiDonHang(string MaDH)
        {
            var MonAn = DataProvider.ISCreated.DB.DonHangs.Where(x => x.MADH == MaDH);
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
        public bool xoaDonHang(string maDH)
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
            List<LoaiMonAn> loaiMonAns = new List<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns);
            foreach (var item in loaiMonAns)
            {
                if (item.ISDEL == 1)
                {
                    loaiMonAns.Remove(item);
                }
            }
            return loaiMonAns;
        }

        public List<LoaiMonAn> danhSachLoaiMonAnTheoTen(string tenLoaiMonAn)
        {
            List<LoaiMonAn> loaiMonAns = new List<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.TENLOAI == tenLoaiMonAn));
            foreach (var item in loaiMonAns)
            {
                if (item.ISDEL == 1)
                {
                    loaiMonAns.Remove(item);
                }
            }
            return loaiMonAns;
        }
        public bool themLoaiMonAn(LoaiMonAn loaiMon)
        {
            if (!tonTaiLoaiMonAn(loaiMon.MALOAI))
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
        public bool xoaLoaiMonAn(string maLoai)
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

    }
}
