using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiQuanCaPhe.Models;
using System.Windows;
using System.Data.Entity;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class HistoryViewModel : NhanVienLayoutViewModelInterface
    {
        public ICommand LoadOrderByCategory { get; set; }
        public ICommand SearchOrder { get; set; }
        public HistoryViewModel()
        {
            Title = "Lịch sử bán hàng";
            SelectedCategory = ListCategory[0];
            LoadOrderByCategory = new RelayCommand<Category>((category) => { return (category != SelectedCategory); }, (category) =>
            {
                SelectedCategory = category;
            });
            SearchOrder = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListOrder = OrderService.GetOrderByQueryString(QueryString);
            });
        }
        private int _OrderSideBar = 0;
        public int OrderSideBar { get => _OrderSideBar; set { OnPropertyChanged(ref _OrderSideBar, value); } }
        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get
            {
                if (_SelectedOrder != null)
                {
                    OrderSideBar = 320;
                }
                else
                {
                    OrderSideBar = 0;
                }
                return _SelectedOrder;
            }
            set
            {
                OnPropertyChanged(ref _SelectedOrder, value);
            }
        }
        private List<Order> _ListOrder = null;
        public List<Order> ListOrder
        {
            get
            {
                if (_ListOrder == null || OrderService.HistoryHasModified())
                {
                    _ListOrder = OrderService.GetOrderByCategory(SelectedCategory);
                }
                return _ListOrder;
            }
            set
            {
                //_ListOrder = value;
                //OnPropertyChanged(null);
                OnPropertyChanged(ref _ListOrder, value);
            }
        }

        // danh muc hien tai
        private Category _SelectedCategory = null;
        public Category SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }
            set
            {
                ListOrder = OrderService.GetOrderByCategory(value);
                OnPropertyChanged(ref _SelectedCategory, value);
            }
        }

        // danh sach danh muc
        private List<Category> _ListCategory;
        public List<Category> ListCategory
        {
            get
            {
                if (_ListCategory == null)
                {
                    _ListCategory = OrderService.GetCategories();
                }
                return _ListCategory;
            }
            set { OnPropertyChanged(ref _ListCategory, value); }
        }

        private string _QueryString;
        public string QueryString
        {
            get { return _QueryString; }
            set
            {
                OnPropertyChanged(ref _QueryString, value);
            }
        }
    }
}
