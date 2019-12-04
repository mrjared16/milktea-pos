using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiQuanCaPhe.Models;
using System.Windows;
namespace QuanLiQuanCaPhe.ViewModels
{
    public class HistoryViewModel : NhanVienLayoutViewModelInterface
    {
        private Order SelectOrder;
        private List<Order> orders;
        public HistoryViewModel()
        {
            Title = "Lịch sử bán hàng";
           

            Drink tmp_item = new Drink("Trà sữa", 30000, "");
            OrderItem tmp_orderItem = new OrderItem(tmp_item, 2, "thêm topping, 30% ngọt, ít đá");
            OrderItem tmp_orderItem2 = new OrderItem(tmp_item, 1, "50% ngọt");
            SelectOrder = new Order("HD001", "24/10/2017 | 13:00:00");
            SelectOrder.Add(tmp_orderItem);
            SelectOrder.Add(tmp_orderItem2);

            orders = new List<Order>();

            orders.Add(SelectOrder);
            orders.Add(SelectOrder);
            orders.Add(SelectOrder);
            orders.Add(SelectOrder);
        }
        public Order Order
        {
            get { return SelectOrder; }
        }
        public List<Order> List
        {
            get { return orders; }
        }
    }
}
