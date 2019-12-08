using Microsoft.Win32;
using QuanLiQuanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
        public ICommand Delete_CancelCommand { get; set; }
        public ICommand SearchMonAnCommand { get; set; }
        public ICommand ChooseImgMonAn { get; set; }

        public List<LoaiMonAn> MilkteaCategories { get; set; }

        private BindingList<MonAn> _listMonAn;
        public BindingList<MonAn> listMonAn
        {
            get { return _listMonAn; }
            set
            {
                _listMonAn = value;
                OnPropertyChanged("listMonAn");
            }
        }

        private string _searchMonAnStr;
        public string searchMonAnStr
        {
            get { return _searchMonAnStr; }
            set
            {
                _searchMonAnStr = value;
                OnPropertyChanged("searchMonAnStr");
            }

        }


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

        private MonAn _MonAnChiTiet;
        public MonAn MonAnChiTiet
        {
            get
            {
                return _MonAnChiTiet;
            }
            set
            {
                _MonAnChiTiet = value;
                OnPropertyChanged("MonAnChiTiet");
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
        private MonAn temp { get; set; }
        public MonAn selectItem_Menu
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
            MonAnChiTiet = new MonAn();
            MonAnChiTiet.TENMON = selectItem_Menu.TENMON;
            MonAnChiTiet.MALOAI = selectItem_Menu.MALOAI;
            MonAnChiTiet.MAMON = selectItem_Menu.MAMON;
            MonAnChiTiet.GIA = selectItem_Menu.GIA;
            MonAnChiTiet.MOTA = selectItem_Menu.MOTA;
            MonAnChiTiet.HINHANH = selectItem_Menu.HINHANH;
            //còn create date, updatedate

            ButtonVisibility = true;
            btnDelete_Cancel = "XÓA";
            btnAdd_Save = "LƯU";
        }

        //itemCombobox loaiMonAn click
        private LoaiMonAn _selectedLoai { get; set; }
        public LoaiMonAn selectedLoai
        {
            get { return _selectedLoai; }
            set
            {
                if (_selectedLoai != value)
                {
                    _selectedLoai = value;

                    showListMonAnTheoLoai();
                }
            }
        }

        public void showListMonAnTheoLoai()
        {
            listMonAn = new BindingList<MonAn>(SeviceData.getListMonAnLoai(selectedLoai.MALOAI));
            ButtonVisibility = false;
        }

        public MonAnAdminViewModel()
        {

            //khi khoi tao thi man hinh chi tiet null
            ButtonVisibility = false;

            //DTO
            MilkteaCategories = new List<LoaiMonAn>();
            MilkteaCategories = SeviceData.getLoaiMonAn();// get from database

            listMonAn = SeviceData.getListMonAn();


            //command  command  command
            Add_SaveCommand = new RelayCommand<Button>((x) =>
            {
                if (string.IsNullOrEmpty(MonAnChiTiet.TENMON)) return false;
                return true;
            },
            (x) =>
            {
                if (x.Content.ToString() == "THÊM")//ADD MON ĂN
                {
                    string res = SeviceData.themMonAn(MonAnChiTiet);
                    MessageBox.Show(res);//xử lí thêm vào

                    if (res.ToString() == "Thành công")
                    {
                        listMonAn.Add(MonAnChiTiet);
                        MonAnChiTiet = new MonAn();
                    }
                }
                else if (x.Content.ToString() == "LƯU")///UPDATE MÓN ĂN
                {
                    string res = SeviceData.suaMonAn(MonAnChiTiet);
                    MessageBox.Show(res);//xử lí thêm vào

                    if (res.ToString() == "Thành công")
                    {
                        selectItem_Menu.TENMON = MonAnChiTiet.TENMON;
                        selectItem_Menu.MAMON = MonAnChiTiet.MAMON;
                        selectItem_Menu.MALOAI = MonAnChiTiet.MALOAI;
                        selectItem_Menu.MOTA = MonAnChiTiet.MOTA;
                        selectItem_Menu.GIA = MonAnChiTiet.GIA;
                        selectItem_Menu.HINHANH = MonAnChiTiet.HINHANH;
                    }
                }

            });
            Delete_CancelCommand = new RelayCommand<Button>((x) => { return true; }, (x) =>
            {
                if (x.Content.ToString() == "HỦY")
                {
                    MonAnChiTiet = new MonAn();
                    ButtonVisibility = false;
                }
                else if (x.Content.ToString() == "XÓA")
                {
                    string res = SeviceData.XoaMonAn(MonAnChiTiet);
                    MessageBox.Show(res);//xử lí thêm vào

                    if (res.ToString() == "Thành công")
                        listMonAn.Remove(selectItem_Menu);
                }

            });

            SearchMonAnCommand = new RelayCommand<Button>((x) =>
            {
                if (string.IsNullOrEmpty(searchMonAnStr)) return false;
                return true;
            },
            (x) =>
            {
                listMonAn = SeviceData.getListMonAnTenMon(searchMonAnStr);
                ButtonVisibility = false;
            });

            ChooseImgMonAn = new RelayCommand<Button>((x) => { return true; }, (x) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    MonAnChiTiet.HINHANH = File.ReadAllBytes(openFileDialog.FileName);
                }
            });

            //click vao them mon
            addMilkteaCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ButtonVisibility = true;
                btnDelete_Cancel = "HỦY";
                btnAdd_Save = "THÊM";
                MonAnChiTiet = new MonAn();
            });

        }
    }
}
