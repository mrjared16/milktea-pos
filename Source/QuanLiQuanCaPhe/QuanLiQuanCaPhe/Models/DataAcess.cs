using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiQuanCaPhe.ViewModel;

namespace QuanLiQuanCaPhe.Models
{
    public class DataAccess
    {
        public static List<Category> GetCategories()
        {
            List<Category> list = new List<Category>(DataProvider.ISCreated.DB.LoaiMonAns.ToList().Where(x => x.ISDEL != 1 && IsDrink(x)).Select(x => new Category(x)));
            list.Insert(0, new Category() { Name = "Tất cả", ID = null });
            return list;
        }

        public static List<Drink> GetDrinkFromCategory(Category category)
        {
            List<Drink> list = DataProvider.ISCreated.DB.MonAns.ToList().Where(x => x.ISDEL != 1 && (category.ID == null || x.MALOAI == category.ID) && IsDrink(x.LoaiMonAn)).Select(x => new Drink(x)).ToList();
            return list;
        }

        public static List<Topping> GetToppings()
        {
            List<Topping> result = DataProvider.ISCreated.DB.LoaiMonAns
                .Where(x => x.ISDEL != 1 && x.TENLOAI.Equals("CÁC LOẠI HẠT"))
                .Join(DataProvider.ISCreated.DB.MonAns, a => a.MALOAI, b => b.MALOAI, (a, b) => b).ToList()
                .Select(x => new Topping(x)).ToList();
            return result;
        }

        public static int GetNextOrderID()
        {
            DonHang LastID = DataProvider.ISCreated.DB.DonHangs.OrderByDescending(a => a.MADH).FirstOrDefault();
            return (LastID == null) ? 0 : LastID.MADH + 1;
            //int id = 0;
            //if (LastID == null || !Int32.TryParse(LastID.MADH, out id))
            //{
            //    return "0";
            //}
            //id++;
            //return id + "";
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
            DataProvider.ISCreated.DB.DonHangs.Add(order.ToDonHang());
            DataProvider.ISCreated.DB.ChiTietDonhangs.AddRange(order.ToChiTietDonHangs());
            DataProvider.ISCreated.DB.SaveChanges();
        }

        // private
        private static List<Order> GetOrderBy(Func<DonHang, bool> Condition)
        {
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

        private static bool IsTopping(LoaiMonAn loaiMonAn)
        {
            return (loaiMonAn.TENLOAI.Equals("CÁC LOẠI HẠT"));
        }
        private static bool IsOption(LoaiMonAn loaiMonAn)
        {
            return false;
        }
        private static bool IsDrink(LoaiMonAn loaiMonAn)
        {
            return (!IsTopping(loaiMonAn) && !IsOption(loaiMonAn));
        }
    }
}
