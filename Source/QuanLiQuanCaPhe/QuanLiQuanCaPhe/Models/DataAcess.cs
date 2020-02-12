using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLiQuanCaPhe.ViewModel;

namespace QuanLiQuanCaPhe.Models
{
    public class DataAccess
    {
        public static List<Category> GetCategories()
        {
            //MessageBox.Show("Query category");
            List<Category> list = new List<Category>(DataProvider.ISCreated.DB.LoaiMonAns.ToList()
                .Where(x => x.ISDEL != 1 && OrderService.IsDrink(x))
                .Select(x => new Category(x)));
            list.Insert(0, new Category() { Name = "Tất cả", ID = null });
            return list;
        }

        public static List<Drink> GetDrinkFromCategory(Category category)
        {
            //MessageBox.Show("Query Drink from Category");
            List<Drink> list = DataProvider.ISCreated.DB.MonAns.ToList()
                .Where(x => x.ISDEL != 1 && (category.ID == null || x.MALOAI == category.ID) && OrderService.IsDrink(x.LoaiMonAn))
                .Select(x => new Drink(x)).ToList();
            return list;
        }

        public static List<Topping> GetToppings()
        {
            //MessageBox.Show("Query Topping");
            List<Topping> result = DataProvider.ISCreated.DB.LoaiMonAns.ToList()
                .Where(x => x.ISDEL != 1 && OrderService.IsTopping(x))
                .Join(DataProvider.ISCreated.DB.MonAns, a => a.MALOAI, b => b.MALOAI, (a, b) => b).ToList()
                .Select(x => new Topping(x)).ToList();
            return result;
        }

        public static List<Order> GetOrderByQueryString(string QueryString)
        {
            List<Order> result = DataProvider.ISCreated.DB.DonHangs.ToList()
                 .Where(x => x.ISDEL != 1 && (ContainDate(x, QueryString) || ContainID(x, QueryString) || ContainStaffName(x, QueryString)))
                 .Select(x => new Order(x)).ToList();
            return result;
        }
        private static bool ContainDate(DonHang donhang, string date)
        {
            string OrderDate = donhang.CREADTEDAT.Value.ToString("dd/MM/yyyy").ToLower();
            return OrderDate.Contains(date);
        }

        private static bool ContainID(DonHang donhang, string ID)
        {
            return donhang.MADH.ToString().Contains(ID);
        }
        private static bool ContainStaffName(DonHang donhang, string name)
        {
            return (donhang.NhanVien.HOTEN.ToLower().Contains(name.ToLower()));
        }
        //public static List<Option> GetOptions()
        //{
        //    List<Option> result = DataProvider.ISCreated.DB.LoaiMonAns.ToList()
        //        .Where(x => x.ISDEL != 1 && OrderService.IsOption(x))
        //        .Join(DataProvider.ISCreated.DB.MonAns, a => a.MALOAI, b => b.MALOAI, (a, b) => b).ToList()
        //        .Select(x => new Option(x)).ToList();
        //    return result;
        //}
        //public static List<OptionGroup> GetGroupsOption()
        //{
        //    List<List<MonAn>> result = DataProvider.ISCreated.DB.LoaiMonAns.ToList()
        //        .Where(x => x.ISDEL != 1 && OrderService.IsOption(x))
        //        .Join(DataProvider.ISCreated.DB.MonAns, a => a.MALOAI, b => b.MALOAI, (a, b) => b).ToList()
        //        .GroupBy(x => x.MALOAI)
        //        .Select(x => x.ToList()).ToList();
        //    List<OptionGroup> result2 = result.Select(x => new OptionGroup(x, GetTenLoai(x))).ToList();
        //    return result2;
        //}
        //private static string GetTenLoai(List<MonAn> list)
        //{
        //    string MaLoai = list.FirstOrDefault().MALOAI;
        //    return DataProvider.ISCreated.DB.LoaiMonAns.FirstOrDefault(x => x.MALOAI == MaLoai).TENLOAI;
        //}
        public static int GetNextOrderID()
        {
            //MessageBox.Show("Query Order ID");
            DonHang LastID = DataProvider.ISCreated.DB.DonHangs.OrderByDescending(a => a.MADH).Where(x => x.ISDEL != 1).FirstOrDefault();
            return (LastID == null) ? 0 : LastID.MADH + 1;
        }

        public static List<Order> GetOrderToday()
        {
            return GetOrderBy(IsInToday);
        }
        public static List<Order> GetOrderThisWeek()
        {
            return GetOrderBy(IsInThisWeek);
        }
        public static List<Order> GetOrderThisMonth()
        {
            return GetOrderBy(IsInThisMonth);
        }
        public static List<Order> GetAllOrder()
        {
            return GetOrderBy((DonHang) => { return true; });
        }

        public static void AddOrder(Order order)
        {
            //MessageBox.Show("Add order");
            DataProvider.ISCreated.DB.DonHangs.Add(order.ToDonHang());
            DataProvider.ISCreated.DB.ChiTietDonhangs.AddRange(order.ToChiTietDonHangs());
            DataProvider.ISCreated.DB.SaveChanges();
        }

        // private
        private static List<Order> GetOrderBy(Func<DonHang, bool> Condition)
        {
            //MessageBox.Show("Query Order");
            List<Order> list =
                DataProvider.ISCreated.DB.DonHangs.ToList()
                .Where(x => Condition(x) && x.ISDEL != 1)
                .Select(x => new Order(x))
                .ToList();
            return list;
        }

        private static bool IsInToday(DonHang DonHang)
        {
            return AreInSameDay(DonHang.CREADTEDAT.Value, DateTime.Now);
        }
        private static bool IsInThisWeek(DonHang DonHang)
        {
            return AreInSameWeek(DonHang.CREADTEDAT.Value, DateTime.Now);
        }
        private static bool IsInThisMonth(DonHang DonHang)
        {
            return AreInSameMonth(DonHang.CREADTEDAT.Value, DateTime.Now);
        }


        private static bool AreInSameDay(DateTime date1, DateTime date2)
        {
            return (date1.Year == date2.Year && date1.Month == date2.Month & date1.Day == date2.Day);
        }
        private static bool AreInSameWeek(DateTime date1, DateTime date2)
        {
            if (date1.Year != date2.Year || date1.Month != date2.Month)
                return false;
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));
            return d1 == d2;
        }
        private static bool AreInSameMonth(DateTime date1, DateTime date2)
        {
            return (date1.Year == date2.Year && date1.Month == date2.Month);
        }


    }
}
