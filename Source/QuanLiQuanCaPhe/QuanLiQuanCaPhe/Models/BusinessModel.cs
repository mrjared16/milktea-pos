using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLiQuanCaPhe.ViewModel;

namespace QuanLiQuanCaPhe.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public Category(LoaiMonAn a)
        {
            this.Name = a.TENLOAI;
            this.ID = a.MALOAI;
        }
        public Category()
        {
        }
    }
    public class Drink
    {
        private double price;
        private string name;
        private byte[] img;
        private string _ID;
        public Drink(string name, float price)
        {
            this.price = price;
            this.name = name;
            this.img = null;
        }
        public Drink(MonAn monan)
        {
            this.price = Convert.ToDouble(monan.GIA);
            this.name = monan.TENMON;
            this.img = monan.HINHANH;
            this.ID = monan.MAMON;
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Name
        {
            get { return name; }
        }
        public string Label
        {
            get { return (name.Length > 15) ? name.Substring(0, 12) + "..." : name; }
        }
        public double Price
        {
            get { return price; }
        }
        public byte[] Image
        {
            get { return img; }
        }
    }
    public class Topping
    {
        public Topping(MonAn MonAn)
        {
            Item = new Drink(MonAn);
        }
        public Topping(Drink Drink)
        {
            Item = Drink;
        }

        private Drink _Item = null;
        public Drink Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
            }
        }
        private bool _Checked = false;

        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
            }
        }
    }


    public class Order : BaseViewModel
    {
        // Create new order
        public Order()
        {
            this.ID = OrderService.GetNextOrderID();
            this.User = UserService.GetCurrentUser;
            this.Date = DateTime.Now;
            this.Coupon = 0;
            this.items = new ObservableCollection<OrderItem>();
        }
        // Retrieve order from database
        public Order(DonHang DonHang)
        {
            this.ID = DonHang.MADH;
            this.User = DonHang.NhanVien;
            this.Date = (DateTime)DonHang.CREADTEDAT;
            this.Coupon = 0;
            this.items = new ObservableCollection<OrderItem>(OrderService.GetOrderItems(DonHang));
        }
        // Add order detail to database
        public DonHang ToDonHang()
        {
            DateTime Now = DateTime.Now;
            return new DonHang()
            {
                MADH = this.ID,
                MANV = this.User.MANV,
                TONGTIEN = this.OrderTotal,
                ISDEL = 0,
                CREADTEDAT = Now,
                UPDATEDAT = Now

            };
        }
        // Add option and topping item to flat list and save to database
        public List<ChiTietDonhang> ToChiTietDonHangs()
        {
            DateTime Now = DateTime.Now;
            List<ChiTietDonhang> list = new List<ChiTietDonhang>();
            foreach (OrderItem item in items)
            {
                list.Add(item.ToChiTietDonHang(this.ID, Now));
                if (item.ToppingsOfItem.Count > 0)
                {
                    foreach (OrderItem topping in item.ToppingsOfItem)
                    {
                        list.Add(topping.ToChiTietDonHang(this.ID, Now));
                    }
                }
            }
            return list;
        }


        // Fields
        public ObservableCollection<OrderItem> items { get; set; }
        public int ID { get; set; }
        public NhanVien User { get; set; }
        public DateTime Date { get; set; }

        private double _Coupon;
        public double Coupon
        {
            get
            {
                return _Coupon;
            }
            set
            {
                OnPropertyChanged(ref _Coupon, value, null);
            }
        }

        // Business fields
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
    }

    public class OrderItem : BaseViewModel
    {
        // Create new order item with drink
        public OrderItem(Drink item, int number = 1)
        {
            ID = OrderService.GetNextOrderItemID();
            Item = item;
            Number = number;
            //Note = note;

            Parent = null;
            ToppingsOfItem = null;
        }

        // Retrieve order item in database
        public OrderItem(ChiTietDonhang ChiTiet)
        {
            this.ID = ChiTiet.ID;
            this._Item = new Drink(ChiTiet.MonAn);
            this.Number = (int)ChiTiet.SOLUONG;
            //this.Note 

            if (ChiTiet.ChiTietDonhang1.Count > 0)
            {
                // TODO: need refactor
                List<OrderItem> Option = ChiTiet.ChiTietDonhang1.Select(x => new OrderItem(x)).ToList();
                this.ToppingsOfItem = new ObservableCollection<OrderItem>(Option);
                this.Parent = null;
            }
            else
            {
                ToppingsOfItem = null;
                //Parent = ...
            }
        }

        // Save order item, if it is topping or option, reference to Parent ID
        public ChiTietDonhang ToChiTietDonHang(int OrderID, DateTime Now)
        {
            ChiTietDonhang result = new ChiTietDonhang()
            {
                ID = this.ID,
                MAMON = this.Item.ID,
                MADH = OrderID,
                SOLUONG = this.Number,
                THANHTIEN = this.ItemTotal,
                ISDEL = 0,
                CREADTEDAT = Now,
                UPDATEDAT = Now,
                PARENTID = (this.Parent == null) ? (int?)null : this.Parent.ID,
                PARENTMADH = (this.Parent == null) ? (int?)null : OrderID
            };

            return result;
        }



        // fields
        public int ID { get; set; }

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

        public OrderItem Parent { get; set; }

        private ObservableCollection<OrderItem> _ToppingOfItems = null;
        public ObservableCollection<OrderItem> ToppingsOfItem
        {
            get
            {
                if (_ToppingOfItems == null)
                {
                    _ToppingOfItems = new ObservableCollection<OrderItem>();
                }
                return _ToppingOfItems;
            }
            set
            {
                OnPropertyChanged(ref _ToppingOfItems, value);
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

        public string Note
        {
            get
            {
                return "";
            }
        }

        // Business fields
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
                double topping = 0;
                foreach (OrderItem toppingItem in ToppingsOfItem)
                {
                    topping += toppingItem.ItemTotal;
                }
                return (Item.Price + topping) * Number;
            }
        }


        // Service
        public void AddParent(OrderItem parent)
        {
            this.Parent = parent;
        }
        public void AddTopping(OrderItem child)
        {
            ToppingsOfItem.Add(child);
        }
        public void RemoveTopping(OrderItem child)
        {
            ToppingsOfItem.Remove(child);
        }
    }

    public class ToppingItem : BaseViewModel
    {
        public ToppingItem(OrderItem orderItem)
        {
            CurentToppingList = new ObservableCollection<Topping>(ListTopping);
            if (orderItem == null || orderItem.ToppingsOfItem == null)
                return;
            //MessageBox.Show(Toppings[0].Item.Name + " - " + Toppings[1].Item.Name);
            foreach (OrderItem toppingItem in orderItem.ToppingsOfItem)
            {
                CheckAnTopping(toppingItem);
            }
        }
        // Fields
        public ObservableCollection<Topping> CurentToppingList { get; set; }
        // public ObservableCollection<Option> Toppings;

        private static List<Topping> _ListTopping = null;
        private static List<Topping> ListTopping
        {
            get
            {
                _ListTopping = OrderService.GetToppings();
                return _ListTopping;
            }

        }

        // Functions
        public void CheckAnTopping(OrderItem currentTopping)
        {
            Topping topping = CurentToppingList.FirstOrDefault(x => x.Item.ID == currentTopping.Item.ID);

            if (topping == null)
            {
                MessageBox.Show("Not found!");
                return;
            }
            topping.Checked = true;
        }

        private void LoadTopping()
        {

        }
    }
}
