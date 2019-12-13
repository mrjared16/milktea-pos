using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiQuanCaPhe.Models;
using System.Windows;
namespace QuanLiQuanCaPhe.ViewModel
{
    public class HistoryViewModel : NhanVienLayoutViewModelInterface
    {
        public ICommand LoadOrderByCategory { get; set; }
		public ICommand SearchDonhang { get; set; }

		private string _QueryStringLichSuDonhang;
		public string QueryStringLichSuDonhang
		{
			get => _QueryStringLichSuDonhang;
			set { _QueryStringLichSuDonhang = value; OnPropertyChanged(); }
		}
		public HistoryViewModel()
        {
            Title = "Lịch sử bán hàng";
            //SelectedOrder = (_ListOrder == null) ? null : ListOrder[0];
            SelectedCategory = ListCategory[0];
            LoadOrderByCategory = new RelayCommand<Category>((category) => { return (category != SelectedCategory); }, (category) =>
           {
               SelectedCategory = category;
           });
			SearchDonhang = new RelayCommand<object>((category) => {
				if(string.IsNullOrEmpty(QueryStringLichSuDonhang))
					return false;
				return true;
			}, (category) =>
			{
				//ListCategory = new List<Category>(SeviceData.TimKiemDonHang(QueryStringLichSuDonhang));
			});

		}
        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get { return _SelectedOrder; }
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
                if (_ListOrder == null)
                    _ListOrder = OrderService.GetOrderByCategory(new Category() { ID = "day"});
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
    }
}
