using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class MonAnAdminViewModel : BaseViewModel
    {
        public ICommand selectedItemListMilk_Clicked { get; set; }
        public ICommand addMilkteaCommand { get; set; }
        public ICommand LoadedMenuUCCommand { get; set; }

        //itemlistView click
        private MilkteaInfo temp { get; set; }
        public MilkteaInfo selectItem_Menu
        {
            get { return temp; }
            set
            {
                if (temp != value)
                {
                    temp = value;
                    showDetails();
                }
            }
        }

        public MilkteaInfo milkTeaInfoCha { get; set; }
        public void showDetails()
        {
            milkTeaInfoCha = selectItem_Menu;
            OnPropertyChanged("milkTeaInfoCha");
            OnPropertyChanged("detailInfoCon");
        }

        public List<Category> MilkteaCategories { get; set; }
        public BindingList<MilkteaInfo> _listMilkteaInfo { get; set; }
        public bool clickOnItemMenu = true;

        private bool _a;
        public bool ButtonVisibility
        {
            get { return _a; }
            set
            {
                _a = value;
                OnPropertyChanged("ButtonVisibility");
            }
        }


        public MonAnAdminViewModel()
        {
            /// <summary>
            /// Command
            /// </summary>
            /// 
            selectedItemListMilk_Clicked = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("knock knock");
            }
           );



            addMilkteaCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("aaa");
            });

            ButtonVisibility = false;

            milkTeaInfoCha = new MilkteaInfo();
            MilkteaCategories = new List<Category>
            {
                new Category
                {
                    Name="Tra sua",

                },
                new Category
                {
                    Name="Hong tra",

                }
            };
            _listMilkteaInfo = new BindingList<MilkteaInfo>();
            for (int i = 0; i < 10; i++)
            {
                MilkteaInfo a = new MilkteaInfo();
                a.tenMon = "Tra sua tran chau " + i.ToString();
                a.gia = 50000 + i * 5000;
                a.imgUrl = "../Image/trasua.jpg";
                _listMilkteaInfo.Add(a);
            }
        }


        public class Category
        {
            public string Name { get; set; }
        }

        public string getImageAbsolutePath(object relativePath)
        {
            string absolutePath =
                $"{AppDomain.CurrentDomain.BaseDirectory}images\\{relativePath}";
            MessageBox.Show(absolutePath);
            return absolutePath;
        }


        public class MilkteaInfo : BaseViewModel
        {
            public string _tenMon;
            public string tenMon
            {
                get { return _tenMon; }
                set
                {
                    _tenMon = value;
                    OnPropertyChanged("tenMon");
                }
            }


            public string _maMon;
            public string maMon
            {
                get { return _maMon; }
                set
                {
                    _maMon = value;
                    OnPropertyChanged("maMon");
                }
            }
            public string _maLoai;
            public string maLoai
            {
                get { return _maLoai; }
                set
                {
                    _maLoai = value;
                    OnPropertyChanged("maLoai");
                }
            }
            public string _moTa;
            public string moTa
            {
                get { return _moTa; }
                set
                {
                    _moTa = value;
                    OnPropertyChanged("moTa");
                }
            }
            public float _gia;
            public float gia
            {
                get { return _gia; }
                set
                {
                    _gia = value;
                    OnPropertyChanged("gia");
                }
            }
            public string _imgUrl;
            public string imgUrl
            {
                get { return _imgUrl; }
                set
                {
                    _imgUrl = value;
                    OnPropertyChanged("imgUrl");
                }
            }
        }
    }
}
