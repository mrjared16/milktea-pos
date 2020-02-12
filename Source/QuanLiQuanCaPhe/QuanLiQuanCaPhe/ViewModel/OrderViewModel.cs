using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiQuanCaPhe.Models;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class OrderViewModel : NhanVienLayoutViewModelInterface
    {

        #region commands
        public ICommand LoadDrinkByCategory { get; set; }
        public ICommand AddDrink { get; set; }
        public ICommand ToggleToppingForDrink { get; set; }
        public ICommand ToggleOptionForDrink { get; set; }
        public ICommand IncreaseAmount { get; set; }
        public ICommand DecreaseAmount { get; set; }
        public ICommand RemoveDrink { get; set; }
        public ICommand ClearOrder { get; set; }
        public ICommand CheckoutOrder { get; set; }
        public ICommand AddCoupon { get; set; }
        public ICommand ShowAddCouponDialog { get; set; }
        public ICommand ToggleOptionView { get; set; }
        public ICommand HideAddCouponDialog { get; set; }
        #endregion

        public OrderViewModel()
        {
            Title = "Bán hàng";
            // commands
            SelectedCategory = ListCategory[0];
            CurrentOrder = new Order(this);

            LoadDrinkByCategory = new RelayCommand<Category>((category) => { return (category != SelectedCategory); }, (category) =>
            {
                SelectedCategory = category;
            });

            AddDrink = new RelayCommand<Drink>((drink) => { return true; }, (drink) =>
            {
                OrderItem ItemOfCurrentDrink = DrinkService.FindDrink(CurrentOrder, drink);

                // only increase amount if item already exist and not having topping or option yet
                // TODO: refactor
                if (ItemOfCurrentDrink != null && !ItemOfCurrentDrink.HasToppings()/* && !ItemOfCurrentDrink.HasOptions()*/)
                {
                    OrderService.SetItemAmount(CurrentOrder, ItemOfCurrentDrink, ItemOfCurrentDrink.Number + 1);
                    return;
                }
                OrderService.AddItem(CurrentOrder, new OrderItem(drink));
            });

            ToggleToppingForDrink = new RelayCommand<Topping>((drink) => { return true; }, (drink) =>
            {
                if (drink.Checked)
                {
                    OrderService.AddTopping(CurrentOrder, drink, SelectedOrderItem);
                }
                else
                {
                    OrderService.RemoveTopping(CurrentOrder, drink, SelectedOrderItem);
                }
            });
            ToggleOptionForDrink = new RelayCommand<Option>((drink) => { return true; }, (drink) =>
            {
                //MessageBox.Show(drink.Item.ID);
                //OrderService.SelectOption(CurrentOrder, drink, SelectedOrderItem);
            });

            IncreaseAmount = new RelayCommand<OrderItem>((drink) => { return true; }, (OrderItem) =>
            {
                OrderService.SetItemAmount(CurrentOrder, OrderItem, OrderItem.Number + 1);
            });

            DecreaseAmount = new RelayCommand<OrderItem>((drink) => { return (drink.Number > 1); }, (OrderItem) =>
            {
                OrderService.SetItemAmount(CurrentOrder, OrderItem, OrderItem.Number - 1);
            });

            RemoveDrink = new RelayCommand<OrderItem>((drink) => { return true; }, (orderitem) =>
            {
                OrderService.RemoveItem(CurrentOrder, orderitem);
            });

            ClearOrder = new RelayCommand<OrderItem>((drink) => { return !OrderService.IsEmpty(CurrentOrder); }, (orderitem) =>
            {
                OrderService.RemoveAllItems(CurrentOrder);
            });

            CheckoutOrder = new RelayCommand<OrderItem>((drink) => { return !OrderService.IsEmpty(CurrentOrder); }, (orderitem) =>
            {
                OrderService.AddOrder(CurrentOrder);
                CurrentOrder.OnPropertyChanged(null);
                CurrentOrder = new Order();
            });

            ShowAddCouponDialog = new RelayCommand<object>((p) => { return !OrderService.IsEmpty(CurrentOrder); }, (p) =>
            {
                IsAddCouponDialogOpen = true;
            });

            HideAddCouponDialog = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OrderService.RemoveCoupon(CurrentOrder);
                IsAddCouponDialogOpen = false;
            });

            AddCoupon = new RelayCommand<object>((p) => { return !OrderService.HasCoupon(CurrentOrder); }, (p) =>
            {
                if (!OrderService.AddCoupon(CurrentOrder, CouponCode))
                {
                    MessageBox.Show("Mã không hợp lệ");
                    return;
                }
                IsAddCouponDialogOpen = false;
            });

            ToggleOptionView = new RelayCommand<OrderItem>((p) => { return true; }, (p) =>
            {
                //if (p == SelectedOrderItem)
                //{
                //    MessageBox.Show("Toggle" + SelectedOrderItem.Item.Name + " - " + p.Item.Name);
                //    OptionSideBar = (OptionSideBar == 0) ? OptionSideBarWidth : 0;
                //}
            });
        }

        // Coupon Dialog
        private string _CouponCode;
        public string CouponCode { get => _CouponCode; set { OnPropertyChanged(ref _CouponCode, value); } }


        private bool _IsAddCouponDiaglogOpen = false;
        public bool IsAddCouponDialogOpen
        {
            get => _IsAddCouponDiaglogOpen;
            set => OnPropertyChanged(ref _IsAddCouponDiaglogOpen, value);
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

                ListDrink = DrinkService.GetDrinkFromCategory(value);
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
                    _ListCategory = DrinkService.GetCategories();
                }
                return _ListCategory;
            }
            set { OnPropertyChanged(ref _ListCategory, value); }
        }

        // danh sach thuc uong cua danh muc
        private List<Drink> _ListDrink = null;
        public List<Drink> ListDrink
        {
            get
            {
                if (_ListDrink == null)
                    _ListDrink = new List<Drink>();
                return _ListDrink;
            }
            set
            {
                OnPropertyChanged(ref _ListDrink, value);
            }
        }
        // Order SideBar toggle
        private int _OrderSideBar = 0;
        private const int OrderSideBarWidth = 320;
        public int OrderSideBar { get => _OrderSideBar; set { OnPropertyChanged(ref _OrderSideBar, value); } }
        private void HideOrderView()
        {
            OrderSideBar = 0;
        }

        private void ShowOrderView()
        {
            OrderSideBar = OrderSideBarWidth;

        }

        public void ToggleOrderView()
        {
            if (CurrentOrder.items.Any())
            {
                //MessageBox.Show("Has an1y");
                this.ShowOrderView();
            }
            else
            {
                //MessageBox.Show("Has nothing");
                this.HideOrderView();
            }
        }
        // Option sidebar toggle
        private int _OptionSideBar = 0;
        private const int OptionSideBarWidth = 200;
        public int OptionSideBar { get => _OptionSideBar; set { OnPropertyChanged(ref _OptionSideBar, value); } }

        // Order hien tai
        private Order _CurrentOrder;
        public Order CurrentOrder
        {
            get
            {
                return _CurrentOrder;
            }
            set
            {
                OnPropertyChanged(ref _CurrentOrder, value);
            }
        }


        // Selected Item to add or remove topping
        private OrderItem _SelectedOrderItem = null;
        public OrderItem SelectedOrderItem
        {
            get
            {
                return _SelectedOrderItem;
            }
            set
            {
                if (value == null)
                {
                    OptionSideBar = 0;
                }
                else
                {
                    OptionSideBar = OptionSideBarWidth;
                }
                _SelectedOrderItem = value;
                // trigger checked items
                OnPropertyChanged(null);
            }
        }

        // Topping option data and binding
        private ToppingItem _DrinkTopping = null;
        public ToppingItem DrinkTopping
        {
            get
            {
                _DrinkTopping = new ToppingItem(SelectedOrderItem);
                return _DrinkTopping;
            }
        }
        //private OptionGroupList _DrinkOption = null;
        //public OptionGroupList DrinkOption
        //{
        //    get
        //    {
        //        _DrinkOption = new OptionGroupList(SelectedOrderItem);
        //        return _DrinkOption;
        //    }
        //}

        //public List<OptionGroup> _ListGroup = null;
        //public List<OptionGroup> ListGroup
        //{
        //    get
        //    {
        //        if (_ListGroup == null)
        //        {
        //            _ListGroup = OrderService.GetGroupsOption();
        //        }
        //        return _ListGroup;
        //    }
        //}
    }


}
