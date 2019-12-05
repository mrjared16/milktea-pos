using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiQuanCaPhe.Models;
using System.Windows;
using QuanLiQuanCaPhe.Models;

namespace QuanLiQuanCaPhe.ViewModels
{
    public class HistoryViewModel : NhanVienLayoutViewModelInterface
    {
        private Order _SelectedOrder;
        private List<Order> _ListOrder = null;
        public HistoryViewModel()
        {
            Title = "Lịch sử bán hàng";

            SelectedOrder = (_ListOrder == null) ? null : ListOrder[0];

        }

        public List<Order> ListOrder
        {
            get
            {
                if (_ListOrder == null)
                {
                    _ListOrder = OrderService.GetListOrder();
                }
                return _ListOrder;
            }
            set
            {
                OnPropertyChanged(ref _ListOrder, value);
            }
        }
        public Order SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                OnPropertyChanged(ref _SelectedOrder, value);
            }
        }
    }
}
