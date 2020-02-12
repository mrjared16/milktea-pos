using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public Nullable<int> ID { get; set; }
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
        private int _ID;
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

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Name
        {
            get { return name; }
        }
        public string CustomName
        {
            get { return Name + " (" + String.Format("{0:n0}", Price) + "đ)"; }
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

        public Topping Clone()
        {
            return new Topping(Item);
        }
    }

    public class Option
    {
        public Option(MonAn MonAn)
        {
            Item = new Drink(MonAn);
        }
        public Option(Drink Drink)
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

        public Option Clone()
        {
            return new Option(Item);
        }
    }

    //public class OptionGroup
    //{
    //    public List<Option> Options { get; set; }
    //    public string GroupName { get; set; }
    //    public OptionGroup(List<Option> option, string Name)
    //    {
    //        Options = new List<Option>(option);
    //        GroupName = Name;
    //    }
    //    public OptionGroup(List<MonAn> monAns, string Name)
    //    {
    //        Options = new List<Option>(monAns.Select(x => new Option(x)));
    //        Options[0].Checked = true;
    //        GroupName = Name;
    //    }
    //    public OptionGroup Clone()
    //    {
    //        List<Option> newOption = Options.ConvertAll(x => x.Clone()).ToList();
    //        return new OptionGroup(newOption, GroupName);
    //    }
    //}

    public class Order : BaseViewModel
    {
        // Create new order
        public Order(OrderViewModel notification = null)
        {
            this.ID = OrderService.GetNextOrderID();
            this.User = UserService.GetCurrentUser;
            this.Date = DateTime.Now;
            this.Coupon = 0;
            this.items = new ObservableCollection<OrderItem>();
            if (notification != null)
            {
                this.items.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => { notification.ToggleOrderView(); };
            }
        }
        // Retrieve order from database
        public Order(DonHang DonHang)
        {
            this.ID = DonHang.MADH;
            this.User = DonHang.NhanVien;
            this.Date = (DateTime)DonHang.CREADTEDAT;
            this.Coupon = (DonHang.GIAMGIA == null) ? 0 : (double)(DonHang.GIAMGIA) ;
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
                GIAMGIA = this.Coupon,
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
                // TODO: refactor
                if (item.HasToppings())
                {
                    foreach (OrderItem topping in item.ToppingsOfItem)
                    {
                        list.Add(topping.ToChiTietDonHang(this.ID, Now));
                    }
                }
                //if (item.HasOptions())
                //{
                //    foreach (OrderItem option in item.OptionsOfItem)
                //    {
                //        list.Add(option.ToChiTietDonHang(this.ID, Now));
                //    }
                //}
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
            //OptionsOfItem = null;
        }

        // Retrieve order item in database
        public OrderItem(ChiTietDonhang ChiTiet)
        {
            this.ID = ChiTiet.ID;

            this._Item = new Drink(ChiTiet.MonAn);
            //MessageBox.Show(_Item.Price + "-");
            this.Number = (int)ChiTiet.SOLUONG;
            //this.Note 
            this.Discount = ChiTiet.GIAMGIA;
            if (HasToppings(ChiTiet))
            {
                // TODO: need refactor
                List<OrderItem> Option = ChiTiet.ChiTietDonhang1.Where(x => OrderService.IsTopping(x.MonAn.LoaiMonAn)).Select(x => new OrderItem(x)).ToList();
                this.ToppingsOfItem = new ObservableCollection<OrderItem>(Option);
                this.Parent = null;
            }
            else
            {
                ToppingsOfItem = null;
                //Parent = ...
            }
            //if (HasOptions(ChiTiet))
            //{
            //    // TODO: need refactor
            //    List<OrderItem> Option = ChiTiet.ChiTietDonhang1.Where(x => OrderService.IsOption(x.MonAn.LoaiMonAn)).Select(x => new OrderItem(x)).ToList();
            //    this.OptionsOfItem = new ObservableCollection<OrderItem>(Option);
            //    this.Parent = null;
            //}
            //else
            //{
            //    OptionsOfItem = null;
            //    //Parent = ...
            //}
        }

        // Save order item, if it is topping or option, reference to Parent ID
        public ChiTietDonhang ToChiTietDonHang(int OrderID, DateTime Now)
        {
            if (this.Parent != null)
                this.Number = this.Parent.Number;
            ChiTietDonhang result = new ChiTietDonhang()
            {
                ID = this.ID,
                MAMON = this.Item.ID,
                MADH = OrderID,
                SOLUONG = this.Number,
                DONGIA = this.Price,
                THANHTIEN = this.Price * this.Number - this.Discount,
                ISDEL = 0,
                CREADTEDAT = Now,
                UPDATEDAT = Now,
                PARENTID = (this.Parent == null) ? (int?)null : this.Parent.ID,
                PARENTMADH = (this.Parent == null) ? (int?)null : OrderID
            };

            return result;
        }


        private double _Discount = 0;
        public double Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                OnPropertyChanged(ref _Discount, value);
            }
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
                    _ToppingOfItems.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => { OnPropertyChanged(null); };
                }
                return _ToppingOfItems;
            }
            set
            {
                OnPropertyChanged(ref _ToppingOfItems, value);
            }
        }

        //private ObservableCollection<OrderItem> _OptionsOfItem = null;
        //public ObservableCollection<OrderItem> OptionsOfItem
        //{
        //    get
        //    {
        //        if (_OptionsOfItem == null)
        //        {
        //            _OptionsOfItem = new ObservableCollection<OrderItem>();
        //            _OptionsOfItem.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => { OnPropertyChanged(null); };
        //        }
        //        return _OptionsOfItem;
        //    }
        //    set
        //    {
        //        OnPropertyChanged(ref _OptionsOfItem, value);
        //    }
        //}
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
                if (this.HasToppings())
                {
                    foreach (OrderItem topping in ToppingsOfItem)
                    {
                        topping.Number = value;
                    }
                }
                //OnPropertyChanged(ref _Number, value);
                OnPropertyChanged("");
            }
        }

        private string _Note = "";
        public string Note
        {
            get
            {
                //if (!OptionsOfItem.Any())
                //    return _Note;
                //return String.Join(", ", OptionsOfItem.Select(p => p.Item.Name));
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

        public double Price
        {
            get
            {
                double option = 0;
                //foreach (OrderItem optionItem in OptionsOfItem)
                //{
                //    option += optionItem.ItemTotal;
                //}

                return option + Item.Price;
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
                double option = 0;
                //foreach (OrderItem optionItem in OptionsOfItem)
                //{
                //    option += optionItem.ItemTotal;
                //}
                return (Item.Price * Number + topping + option) - Discount;
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
        //public void AddOption(OrderItem child)
        //{

        //    OptionsOfItem.Add(child);
        //}
        //public void RemoveOption(OrderItem child)
        //{
        //    OptionsOfItem.Remove(child);
        //}

        public bool HasToppings()
        {
            return (ToppingsOfItem.Count > 0);
        }
        //public bool HasOptions()
        //{
        //    return (OptionsOfItem.Count > 0);
        //}

        private bool HasToppings(ChiTietDonhang chiTietDonhang)
        {
            return chiTietDonhang.ChiTietDonhang1.Count > 0;
        }
        //private bool HasOptions(ChiTietDonhang chiTietDonhang)
        //{
        //    return chiTietDonhang.ChiTietDonhang1.Count > 0;
        //    //return OrderSer
        //    //return false;
        //}
    }

    public class ToppingItem : BaseViewModel
    {
        public ToppingItem(OrderItem orderItem)
        {
            CurrentToppingList = new ObservableCollection<Topping>(ListTopping.ConvertAll(topping => topping.Clone()));
            // TODO: refactor
            if (orderItem == null || orderItem.ToppingsOfItem == null)
                return;
            foreach (OrderItem toppingItem in orderItem.ToppingsOfItem)
            {
                CheckAnTopping(toppingItem);
            }
        }
        // Fields
        public ObservableCollection<Topping> CurrentToppingList { get; set; }
        // public ObservableCollection<Option> Toppings;

        private static List<Topping> _ListTopping = null;
        private static List<Topping> ListTopping
        {
            get
            {
                if (_ListTopping == null)
                    _ListTopping = OrderService.GetToppings();
                return _ListTopping;
            }

        }

        // Functions
        public void CheckAnTopping(OrderItem currentTopping)
        {
            Topping topping = CurrentToppingList.FirstOrDefault(x => x.Item.ID == currentTopping.Item.ID);

            if (topping == null)
            {
                MessageBox.Show("Not found!");
                return;
            }
            topping.Checked = true;
        }
    }
    //public class OptionGroupList : BaseViewModel
    //{
    //     public OptionGroupList(OrderItem orderItem)
    //    {
    //        CurrentOptionList = new ObservableCollection<OptionGroup>(ListOptionGroup.ConvertAll(option => option.Clone()));
    //        // TODO: refactor
    //        if (orderItem == null || orderItem.OptionsOfItem == null)
    //            return;
    //        foreach (OrderItem toppingItem in orderItem.OptionsOfItem)
    //        {
    //            CheckOptions(toppingItem);
    //        }
    //    }
    //    public ObservableCollection<OptionGroup> CurrentOptionList { get; set; }
    //    private static List<OptionGroup> _ListOptionGroup = null;
    //    private static List<OptionGroup> ListOptionGroup
    //    {
    //        get
    //        {
    //            if (_ListOptionGroup == null)
    //                _ListOptionGroup = OrderService.GetGroupsOption();
    //            return _ListOptionGroup;
    //        }

    //    }
    //    public void CheckOptions(OrderItem currentOption)
    //    {
    //        Option topping = CurrentOptionList.Select(x => x.Options)
    //            .Select(x => x.FirstOrDefault(y => y.Item.ID == currentOption.Item.ID)).FirstOrDefault();

    //        if (topping == null)
    //        {
    //            MessageBox.Show("Not found!");
    //            return;
    //        }
    //        topping.Checked = true;
    //    }
    //}
    //public class OptionItem : BaseViewModel
    //{
    //    public OptionItem(OrderItem orderItem)
    //    {
    //        CurrentOptionList = new ObservableCollection<Option>(ListOption.ConvertAll(option => option.Clone()));
    //        // TODO: refactor
    //        if (orderItem == null || orderItem.OptionsOfItem == null)
    //            return;
    //        foreach (OrderItem toppingItem in orderItem.OptionsOfItem)
    //        {
    //            CheckAnOption(toppingItem);
    //        }
    //    }
    //    // Fields
    //    public ObservableCollection<Option> CurrentOptionList { get; set; }
    //    // public ObservableCollection<Option> Options;

    //    private static List<Option> _ListOption = null;
    //    public static List<Option> ListOption
    //    {
    //        get
    //        {
    //            if (_ListOption == null)
    //                _ListOption = OrderService.GetOptions();
    //            return _ListOption;
    //        }

    //    }

    //    // Functions
    //    public void CheckAnOption(OrderItem currentOption)
    //    {
    //        Option topping = CurrentOptionList.FirstOrDefault(x => x.Item.ID == currentOption.Item.ID);

    //        if (topping == null)
    //        {
    //            MessageBox.Show("Not found!");
    //            return;
    //        }
    //        topping.Checked = true;
    //    }

    //}

}
