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
        public ICommand addMilkteaCommand { get; set; }
        public ICommand LoadedMenuUCCommand { get; set; }
        public ICommand Add_SaveCommand { get; set; }

        public List<Category> MilkteaCategories { get; set; }
        public BindingList<MilkteaInfo> _listMilkteaInfo { get; set; }


        #region Binding
        private bool _ButtonVisibility;
        public bool ButtonVisibility
        {
            get { return _ButtonVisibility; }
            set
            {
                _ButtonVisibility = value;
                OnPropertyChanged("ButtonVisibility");
            }
        }

        private MilkteaInfo _milkTeaInfoCha;
        public MilkteaInfo milkTeaInfoCha
        {
            get
            {
                return _milkTeaInfoCha;
            }
            set
            {
                _milkTeaInfoCha = value;
                OnPropertyChanged("milkTeaInfoCha");
            }
        }

        private string _btnAdd_Save;
        public string btnAdd_Save
        {
            get { return _btnAdd_Save; }
            set
            {
                _btnAdd_Save = value;
                OnPropertyChanged("btnAdd_Save");
            }
        }

        private string _btnDelete_Cancel;
        public string btnDelete_Cancel
        {
            get { return _btnDelete_Cancel; }
            set
            {
                _btnDelete_Cancel = value;
                OnPropertyChanged("btnDelete_Cancel");
            }
        }
        #endregion


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

        public void showDetails()
        {
            milkTeaInfoCha = new MilkteaInfo();
            milkTeaInfoCha = selectItem_Menu;
            ButtonVisibility = true;
            btnDelete_Cancel = "XÓA";
            btnAdd_Save = "LƯU";

        }

        public MonAnAdminViewModel()
        {

            Add_SaveCommand = new RelayCommand<Object>((x) => { return true; }, (x) =>
            {
                MessageBox.Show("hihi");
            });
            //khi khoi tao thi man hinh chi tiet null
            ButtonVisibility = false;

            //click vao them mon
            addMilkteaCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ButtonVisibility = true;
                btnDelete_Cancel = "HỦY";
                btnAdd_Save = "THÊM";
                milkTeaInfoCha = new MilkteaInfo();
                Add_SaveCommand = new RelayCommand<Object>((x) => { return true; }, (x) =>
                {
                    MessageBox.Show(milkTeaInfoCha.tenMon);
                });
            });




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
            for (int i = 0; i < 15; i++)
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
