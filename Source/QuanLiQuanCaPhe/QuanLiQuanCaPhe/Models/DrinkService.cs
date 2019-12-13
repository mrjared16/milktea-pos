using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
    public class DrinkService
    {
        // API
        public static List<Category> GetCategories()
        {
            return DataAccess.GetCategories();
        }
        public static List<Drink> GetDrinkFromCategory(Category category)
        {
            return DataAccess.GetDrinkFromCategory(category);
        }
        public static OrderItem FindDrink(Order order, Drink Drink)
        {
            return order.items.Where(x => x.Item.ID == Drink.ID).LastOrDefault();
        }
    }
}
