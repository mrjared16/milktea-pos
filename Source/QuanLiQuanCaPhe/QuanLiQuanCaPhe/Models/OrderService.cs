using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLiQuanCaPhe.ViewModel;

namespace QuanLiQuanCaPhe.Models
{

    public class OrderService
    {
        public static int GetNextOrderID()
        {
            return DataAccess.GetNextOrderID();
        }
        public static int GetNextOrderItemID()
        {
            return _OrderItemID++;
        }

        public static List<Category> GetCategories()
        {
            List<Category> _ListCategory = new List<Category>();
            _ListCategory.Add(new Category() { Name = "Hôm nay", ID = 1 });
            _ListCategory.Add(new Category() { Name = "Tuần này", ID = 2 });
            _ListCategory.Add(new Category() { Name = "Tháng này", ID = 3 });
            _ListCategory.Add(new Category() { Name = "Tất cả", ID = null });
            return _ListCategory;
        }
        public static List<Order> GetOrderByCategory(Category value)
        {
            if (value == null)
                return null;
            switch (value.ID)
            {
                case 1:
                    return DataAccess.GetOrderToday();
                case 2:
                    return DataAccess.GetOrderThisWeek();
                case 3:
                    return DataAccess.GetOrderThisMonth();
                default:
                    return DataAccess.GetAllOrder();
            }
        }
        public static List<Order> GetOrderByQueryString(string QueryString)
        {
            return DataAccess.GetOrderByQueryString(QueryString);
        }
        
        public static List<OrderItem> GetOrderItems(DonHang DonHang)
        {
            List<OrderItem> result = DonHang.ChiTietDonhangs.ToList().Where(x => x.ISDEL != 1 && IsDrink(x)).Select(x => new OrderItem(x)).ToList();
            return result;
        }

        public static List<Topping> GetToppings()
        {
            return DataAccess.GetToppings();
        }

        //public static List<Option> GetOptions()
        //{
        //    return DataAccess.GetOptions();
        //}

        //public static List<OptionGroup> GetGroupsOption()
        //{
        //    return DataAccess.GetGroupsOption();
        //}
        public static bool HistoryHasModified()
        {
            if (!_HistoryHasModified)
                return false;

            _HistoryHasModified = false;
            return true;
        }
        public static double ValidateCoupon(string CouponCode)
        {
            // TODO: refactor
            if (CouponCode == "MAGIAMGIA")
            {
                return 20;
            }
            return 0;
        }


        public static void AddOrder(Order item)
        {
            _OrderItemID = 0;
            _HistoryHasModified = true;
            DataAccess.AddOrder(item);
        }
        // order item
        public static void AddItem(Order order, OrderItem item)
        {
            order.items.Add(item);
            order.OnPropertyChanged(null);
        }
        public static void RemoveItem(Order order, OrderItem item)
        {
            order.items.Remove(item);
            order.OnPropertyChanged(null);
        }



        public static void SetItemAmount(Order order, OrderItem orderitem, int amount)
        {
            orderitem.Number = amount;
            order.OnPropertyChanged(null);
        }

        public static void RemoveAllItems(Order order)
        {
            order.items.Clear();
            order.OnPropertyChanged(null);
        }

        // topping
        public static void AddTopping(Order order, Topping topping, OrderItem parent)
        {
            // TODO: refactor
            if (topping == null || parent == null)
            {
                MessageBox.Show("Co loi xay ra");
                return;
            }
            //TODO: refactor
            OrderItem orderItem = new OrderItem(topping.Item, parent.Number);
            parent.AddTopping(orderItem);
            orderItem.AddParent(parent);
            order.OnPropertyChanged(null);
        }
        public static void RemoveTopping(Order order, Topping topping, OrderItem parent)
        {
            // TODO: refactor
            if (topping == null || parent == null)
            {
                MessageBox.Show("Co loi xay ra");
                return;
            }
            // TODO: refactor
            OrderItem orderItem = parent.ToppingsOfItem.FirstOrDefault(x => x.Item.ID == topping.Item.ID);
            parent.RemoveTopping(orderItem);
            order.OnPropertyChanged(null);
        }

        // Option
        //public static void SelectOption(Order currentOrder, Option drink, OrderItem option)
        //{
        //    // TODO: refactor
        //    if (drink == null || option == null)
        //    {
        //        MessageBox.Show("Co loi xay ra");
        //        return;
        //    }
        //    OrderItem old = option.OptionsOfItem.FirstOrDefault(x => HasSameCategory(drink.Item, x.Item));
        //    if (old != null)
        //        RemoveOption(currentOrder, old.Item, option);
        //    //TODO: refactor
        //    OrderItem orderItem = new OrderItem(drink.Item);

        //    option.AddOption(orderItem);
        //    orderItem.AddParent(option);
        //    currentOrder.OnPropertyChanged(null);
        //}

        //public static void RemoveOption(Order currentOrder, Drink drink, OrderItem parent)
        //{
        //    // TODO: refactor
        //    if (drink == null || parent == null)
        //    {
        //        MessageBox.Show("Co loi xay ra");
        //        return;
        //    }
        //    // TODO: refactor
        //    OrderItem orderItem = parent.OptionsOfItem.FirstOrDefault(x => x.Item.ID == drink.ID);
        //    parent.RemoveOption(orderItem);
        //    currentOrder.OnPropertyChanged(null);
        //}

        //private static bool HasSameCategory(Drink a, Drink b)
        //{
        //    return GetMonAn(a).MALOAI == GetMonAn(b).MALOAI;
        //}
        //private static MonAn GetMonAn(Drink a)
        //{
        //    return DataProvider.ISCreated.DB.MonAns.FirstOrDefault(x => x.MAMON == a.ID);
        //}
        // coupon
        public static bool AddCoupon(Order order, string Coupon)
        {
            order.Coupon = OrderService.ValidateCoupon(Coupon);
            if (order.Coupon == 0)
                return false;
            return true;
        }
        public static void RemoveCoupon(Order order)
        {
            order.Coupon = 0;
        }

        public static bool IsEmpty(Order order)
        {
            return !order.items.Any();
        }
        public static bool HasCoupon(Order order)
        {
            return order.Coupon > 0;
        }
        public static bool IsTopping(LoaiMonAn loaiMonAn)
        {
            return (loaiMonAn.TENLOAI.Equals("CÁC LOẠI HẠT"));
        }
        public static bool IsOption(LoaiMonAn loaiMonAn)
        {
            return (loaiMonAn.TENLOAI.Equals("SIZE") || loaiMonAn.TENLOAI.Equals("TYPE"));
        }
        public static bool IsDrink(LoaiMonAn loaiMonAn)
        {
            return (!IsTopping(loaiMonAn) && !IsOption(loaiMonAn));
        }


        private static int _OrderItemID = 0;
        private static bool _HistoryHasModified = false;
        private static bool IsDrink(ChiTietDonhang chiTietDonhang)
        {
            return chiTietDonhang.ChiTietDonhang2 == null;
        }

    }


}
