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
        public ICommand IncreaseAmount { get; set; }
        public ICommand DecreaseAmount { get; set; }
        public ICommand RemoveDrink { get; set; }
        public ICommand ClearOrder { get; set; }
        public ICommand CheckoutOrder { get; set; }
        public ICommand AddCoupon { get; set; }
        public ICommand ShowAddCouponDialog { get; set; }
        public ICommand HideAddCouponDialog { get; set; }
        #endregion

        public OrderViewModel()
        {
            Title = "Bán hàng";
            // commands
            SelectedCategory = ListCategory[0];
            LoadDrinkByCategory = new RelayCommand<Category>((category) => { return (category != SelectedCategory); }, (category) =>
            {
                SelectedCategory = category;
            });
            AddDrink = new RelayCommand<Drink>((drink) => { return true; }, (drink) =>
            {
                OrderItem OrderItem = CurrentOrder.FindDrink(drink);
                if (OrderItem != null)
                {
                    CurrentOrder.SetAmount(OrderItem, OrderItem.Number + 1);
                    return;
                }

                CurrentOrder.Add(new OrderItem(drink, 1, ""));
            });
            IncreaseAmount = new RelayCommand<OrderItem>((drink) => { return true; }, (OrderItem) =>
            {
                CurrentOrder.SetAmount(OrderItem, OrderItem.Number + 1);
            });
            DecreaseAmount = new RelayCommand<OrderItem>((drink) => { return (drink.Number > 1); }, (OrderItem) =>
            {
                CurrentOrder.SetAmount(OrderItem, OrderItem.Number - 1);
            });
            RemoveDrink = new RelayCommand<OrderItem>((drink) => { return true; }, (orderitem) =>
            {
                CurrentOrder.Remove(orderitem);
            });
            ClearOrder = new RelayCommand<OrderItem>((drink) => { return !CurrentOrder.IsEmpty(); }, (orderitem) =>
            {
                CurrentOrder.RemoveAll();
            });
            CheckoutOrder = new RelayCommand<OrderItem>((drink) => { return !CurrentOrder.IsEmpty(); }, (orderitem) =>
            {
                CurrentOrder.SaveOrder();
                CurrentOrder = new Order();
            });

            ShowAddCouponDialog = new RelayCommand<object>((p) => { return !CurrentOrder.IsEmpty(); }, (p) =>
            {
                IsAddCouponDialogOpen = true;
            });

            HideAddCouponDialog = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentOrder.ClearCoupon();
                IsAddCouponDialogOpen = false;
            });

            AddCoupon = new RelayCommand<object>((p) => { return !CurrentOrder.hasCoupon(); }, (p) =>
            {
                if (!CurrentOrder.AddCoupon(CouponCode))
                {
                    MessageBox.Show("Mã không hợp lệ");
                    return;
                }
                IsAddCouponDialogOpen = false;
            });
        }
        private string _CouponCode;
        public string CouponCode { get => _CouponCode; set { OnPropertyChanged(ref _CouponCode, value); } }

        private int _OrderSideBar = 0;
        public int OrderSideBar { get => _OrderSideBar; set { OnPropertyChanged(ref _OrderSideBar, value); } }

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


        private Order _CurrentOrder = null;
        public Order CurrentOrder
        {
            get
            {
                if (_CurrentOrder == null)
                {
                    _CurrentOrder = new Order();
                    _CurrentOrder.items.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs args) =>
                    {
                        if (_CurrentOrder.items.Any())
                        {
                            OrderSideBar = 320;
                        }
                        else
                        {
                            OrderSideBar = 0;
                        }
                    };

                }
                return _CurrentOrder;
            }
            set
            {

                OnPropertyChanged(ref _CurrentOrder, value);
            }
        }
    }
    public class OrderItem : BaseViewModel
    {
        private Drink _Item;
        public Drink Item
        {
            get
            {
                return _Item;
            }
            set
            {
                OnPropertyChanged(ref _Item, value);
            }
        }
        private int _Number = 0;
        public int Number
        {
            get
            {
                return _Number;
            }
            set
            {
                _Number = value;
                //OnPropertyChanged(ref _Number, value);
                OnPropertyChanged("");
            }
        }
        private string _Note = "";
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                OnPropertyChanged(ref _Note, value);
            }
        }
        public string Info
        {
            get
            {
                return ((this.Note != "") ? " (" + this.Note + ")" : "");
            }
        }
        public double ItemTotal
        {
            get
            {
                return Item.Price * Number;
            }
        }

        public OrderItem(Drink item, int number, string note)
        {
            Item = item;
            Number = number;
            Note = note;
        }
        public OrderItem(ChiTietDonhang ChiTiet)
        {
            this._Item = new Drink(ChiTiet.MonAn);
            this.Number = (int)ChiTiet.SOLUONG;

            //this.Note = "";
        }
        public ChiTietDonhang ToChiTietDonHang(string OrderID, DateTime Now)
        {
            return new ChiTietDonhang()
            {
                MADH = OrderID,
                MAMON = this.Item.ID,
                SOLUONG = this.Number,
                THANHTIEN = this.ItemTotal,
                ISDEL = 0,
                CREADTEDAT = Now,
                UPDATEDAT = Now

            };
        }
    }
    public class Order : BaseViewModel
    {
        public Order(string ID, DateTime date, string username, float coupon)
        {
            this.ID = ID;
            this.Date = date;
            //this.Username = username;
            this.Coupon = coupon;
            items = new ObservableCollection<OrderItem>();
        }
        public Order()
        {

            this.Date = DateTime.Now;
            //this.Username = OrderService.GetUser();
            this.Coupon = 0;
            items = new ObservableCollection<OrderItem>();
            this.User = UserService.GetCurrentUser;
        }
        public Order(DonHang DonHang)
        {
            this.ID = DonHang.MADH;
            this.User = DonHang.NhanVien;
            this.Date = (DateTime)DonHang.CREADTEDAT;
            this.Coupon = 0;
            this.items = new ObservableCollection<OrderItem>(OrderService.GetOrderItems(DonHang));
        }
        public void Add(OrderItem item)
        {
            items.Add(item);
            OnPropertyChanged(null);
        }
        public void Remove(OrderItem item)
        {
            items.Remove(item);
            OnPropertyChanged(null);
        }
        public void RemoveAll()
        {
            items.Clear();
            OnPropertyChanged(null);
        }
        public void SaveOrder()
        {
            OrderService.AddOrder(this);
            OnPropertyChanged(null);
        }
        // TODO: need refactor
        public void SetAmount(OrderItem orderitem, int amount)
        {
            orderitem.Number = amount;
            OnPropertyChanged(null);
        }
        public OrderItem FindDrink(Drink Drink)
        {
            return items.Where(x => x.Item.ID == Drink.ID).FirstOrDefault();
        }

        public bool AddCoupon(string Coupon)
        {
            this.Coupon = OrderService.ValidateCoupon(Coupon);
            return (this.Coupon != 0);
        }
        public void ClearCoupon()
        {
            Coupon = 0;
        }
        // public binding data
        public ObservableCollection<OrderItem> items { get; set; }
        public string ID { get; set; }
        private double _Coupon;
        public double Coupon
        {
            get
            {
                return _Coupon;
            }
            set
            {
                OnPropertyChanged(ref _Coupon, value);
            }
        }
        public DateTime Date { get; set; }
        public NhanVien User { get; set; }
        public string Username
        {
            get
            {
                return this.User.HOTEN;
            }
        }
        public double OrderSubTotal
        {
            get
            {
                double s = 0;
                foreach (OrderItem orderitem in items)
                {
                    s += orderitem.ItemTotal;
                }
                return s;
            }
        }
        public double CouponAmount
        {
            get
            {
                return (OrderSubTotal * Coupon / 100);
            }
        }
        public double OrderTotal
        {
            get
            {
                return OrderSubTotal * (1 - Coupon / 100);
            }
        }

        public DonHang ToDonHang()
        {
            DateTime Now = DateTime.Now;
            this.ID = OrderService.GetNextOrderID();
            return new DonHang()
            {
                MADH = this.ID,
                MANV = this.User.MANV,
                THOIGIAN = Now,
                TONGTIEN = this.OrderTotal,
                ISDEL = 0,
                CREADTEDAT = Now,
                UPDATEDAT = Now

            };
        }
        public List<ChiTietDonhang> ToChiTietDonHangs()
        {
            DateTime Now = DateTime.Now;
            List<ChiTietDonhang> list = new List<ChiTietDonhang>();
            foreach (OrderItem item in items)
            {
                list.Add(item.ToChiTietDonHang(this.ID, Now));
            }
            return list;
        }

        public bool IsEmpty()
        {
            return !items.Any();
        }
        public bool hasCoupon()
        {
            return Coupon > 0;
        }
    }
}
