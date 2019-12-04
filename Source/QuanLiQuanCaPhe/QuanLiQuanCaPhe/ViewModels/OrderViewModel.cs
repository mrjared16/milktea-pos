using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModels
{
    public class OrderViewModel : NhanVienLayoutViewModelInterface
    {

        #region commands
        public ICommand LoadCategory { get; set; }
        public ICommand AddDrink { get; set; }
        public ICommand IncreaseAmount { get; set; }
        public ICommand DecreaseAmount { get; set; }
        public ICommand RemoveDrink { get; set; }
        public ICommand ClearOrder { get; set; }
        public ICommand CheckoutOrder { get; set; }
        #endregion

        public OrderViewModel()
        {
            Title = "Bán hàng";
            // commands
            SelectedCategory = ListCategory[0];
            LoadCategory = new RelayCommand<Category>((category) => { return (category != SelectedCategory); }, (category) =>
            {
                SelectedCategory = category;
            });
            AddDrink = new RelayCommand<Drink>((drink) => { return true; }, (drink) =>
            {
                CurrentOrder.Add(new OrderItem(drink, 1, ""));
            });
            IncreaseAmount = new RelayCommand<OrderItem>((drink) => { return true; }, (OrderItem) =>
            {
                CurrentOrder.SetAmount(OrderItem, OrderItem.Number + 1);
                //OrderItem.Number++;
            });
            DecreaseAmount = new RelayCommand<OrderItem>((drink) => { return (drink.Number > 1); }, (OrderItem) =>
            {
                CurrentOrder.SetAmount(OrderItem, OrderItem.Number - 1);
                //OrderItem.Number--;
            });
            RemoveDrink = new RelayCommand<OrderItem>((drink) => { return true; }, (orderitem) =>
            {
                CurrentOrder.Remove(orderitem);
            });
            ClearOrder = new RelayCommand<OrderItem>((drink) => { return true; }, (orderitem) =>
            {
                CurrentOrder.RemoveAll();
            });
            CheckoutOrder = new RelayCommand<OrderItem>((drink) => { return true; }, (orderitem) =>
            {
                CurrentOrder.SaveOrder();
                CurrentOrder = new Order();
            });

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
                ListDrink = DrinkService.getDrinkFromCategory(value);
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
                    _ListCategory = DrinkService.getCategory();
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
                }
                return _CurrentOrder;
            }
            set
            {
                OnPropertyChanged(ref _CurrentOrder, value);
            }
        }
    }
    public class Category
    {
        public string Name { get; set; }
    }
    public class DrinkService
    {
        public static List<Category> getCategory()
        {
            List<Category> _ListCategory = new List<Category>();
            _ListCategory.Add(new Category() { Name = "Tất cả" });
            _ListCategory.Add(new Category() { Name = "Trà sữa" });
            _ListCategory.Add(new Category() { Name = "Trà trái cây" });
            _ListCategory.Add(new Category() { Name = "Coffee" });
            return _ListCategory;
        }
        public static List<Drink> getDrinkFromCategory(Category _ListCategory)
        {
            return getDrinkFromCategory(_ListCategory.Name);
        }
        private static List<Drink> getDrinkFromCategory(string _ListCategory)
        {
            List<Drink> result = new List<Drink>();
            for (int i = 1; i <= 15; i++)
                result.Add(new Drink(_ListCategory + i, 30000, ""));
            return result;
        }
    }
    public class Drink
    {
        private float price;
        private string name;
        private string img_source;

        public Drink(string name, float price, string img_source)
        {
            this.price = price;
            this.name = name;
            this.img_source = img_source;
        }

        public string Name
        {
            get { return name; }
        }
        public float Price
        {
            get { return price; }
        }
        public string ImgSource()
        {
            return img_source;
        }
    }


    public class OrderService
    {
        public static int counter = 1;
        public static Order createOrder()
        {
            Drink tmp_item = new Drink("Trà sữa", 30000, "");
            OrderItem tmp_orderItem = new OrderItem(tmp_item, 2, "thêm topping, 30% ngọt, ít đá");
            OrderItem tmp_orderItem2 = new OrderItem(tmp_item, 1, "50% ngọt");
            Order _CurrentOrder = new Order("HD001", "24/10/2017 | 13:00:00");
            _CurrentOrder.Add(tmp_orderItem);
            _CurrentOrder.Add(tmp_orderItem2);
            return _CurrentOrder;
        }
        public static string getNextID()
        {
            return "HD" + String.Format("{0:000}", counter++);
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
        public float ItemTotal
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
    }
    public class Order : BaseViewModel
    {
        public Order(string ID, string date)
        {
            this.ID = ID;
            //this.Date = date;
            this.Coupon = 10;
            items = new ObservableCollection<OrderItem>();
        }
        public Order()
        {
            this.ID = OrderService.getNextID();
            this.Date = DateTime.Now;
            items = new ObservableCollection<OrderItem>();
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
            // do something here
            OnPropertyChanged(null);
        }
        // TODO: need refactor
        public void SetAmount(OrderItem orderitem, int amount)
        {
            orderitem.Number = amount;
            OnPropertyChanged(null);
        }
        public ObservableCollection<OrderItem> items { get; set; }
        public string ID { get; set; }
        public float Coupon { get; set; }
        public DateTime Date { get; set; }
        public float OrderSubTotal
        {
            get
            {
                float s = 0;
                foreach (OrderItem orderitem in items)
                {
                    s += orderitem.ItemTotal;
                }
                return s;
            }
        }
        public float CouponAmount
        {
            get
            {
                return (OrderSubTotal * Coupon / 100);
            }
        }
        public float OrderTotal
        {
            get
            {
                return OrderSubTotal * (1 - Coupon / 100);
            }
        }
    }
}
