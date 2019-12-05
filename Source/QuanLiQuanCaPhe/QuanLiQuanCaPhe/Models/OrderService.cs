using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiQuanCaPhe.ViewModels;

namespace QuanLiQuanCaPhe.Models
{

    public class OrderService
    {
        // API
        public static string GetUser()
        {
            return "Phúc";
        }
        public static void AddOrder(Order item)
        {
            ListOrder.Add(item);
        }
        public static string GetNextID()
        {
            return FakeData.GetNextID;
        }


        private static List<Order> ListOrder = null;
        public static List<Order> GetListOrder()
        {
            if (ListOrder == null)
            {
                ListOrder = new List<Order>();
                //ListOrder.Add(FakeData.createOrder());
                //ListOrder.Add(FakeData.createOrder());
            }
            return ListOrder;
        }


    }

    public class DrinkService
    {
        // API
        public static List<Category> GetCategories()
        {
            return FakeData.GetCategories();
        }
        public static List<Drink> GetDrinkFromCategory(Category _ListCategory)
        {
            return FakeData.GetDrinkFromCategory(_ListCategory.Name);
        }
    }


    public class FakeData
    {
        //fake data
        public static List<Drink> GetDrinkFromCategory(string _ListCategory)
        {
            List<Drink> result = new List<Drink>();
            for (int i = 1; i <= 15; i++)
                result.Add(new Drink(_ListCategory + i, 30000, ""));
            return result;
        }
        public static List<Category> GetCategories()
        {
            List<Category> _ListCategory = new List<Category>();
            _ListCategory.Add(new Category() { Name = "Tất cả" });
            _ListCategory.Add(new Category() { Name = "Trà sữa" });
            _ListCategory.Add(new Category() { Name = "Trà trái cây" });
            _ListCategory.Add(new Category() { Name = "Coffee" });
            return _ListCategory;
        }

        // fake data generate
        public static int counter = 1;
        public static string[] user = { "Phúc", "Tú", "Nguyên", "Quyên" };

        public static Order CreateOrder()
        {
            Order _CurrentOrder = new Order(GetNextID, DateTime.Now, GetNextUsername, GetCoupon);
            for (int i = 0; i < counter; i++)
            {
                OrderItem tmp_orderItem = new OrderItem(new Drink("Trà sữa " + counter, 30000, ""), 2, "thêm toppinG, 30% ngọt, ít đá");
                _CurrentOrder.Add(tmp_orderItem);
            }
            return _CurrentOrder;
        }
        public static string GetNextID
        {
            get { return "HD" + String.Format("{0:000}", counter++); }
        }
        private static string GetNextUsername
        {
            get { return user[counter % user.Length]; }
        }
        private static float GetCoupon
        {
            get { return (counter % 9) * 10; }
        }
    }
}
