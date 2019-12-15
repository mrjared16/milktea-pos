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
        public ICommand ShowAllMonAn { get; set; }

        private BindingList<LoaiMonAn> _MilkteaCategories;
        public BindingList<LoaiMonAn> MilkteaCategories
        {
            get
            {
                return new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database
                ;
            }
            set
            {
                _MilkteaCategories = value;
                // _MilkteaCategories = new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database
                OnPropertyChanged("MilkteaCategories");
            }
        }

        private BindingList<LoaiMonAn> _MilkteaCategoriesDetails;
        public BindingList<LoaiMonAn> MilkteaCategoriesDetails
        {
            get
            {
                return new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database
                ;
            }
            set
            {
                _MilkteaCategoriesDetails = value;
                OnPropertyChanged("MilkteaCategoriesDetails");
                //_MilkteaCategoriesDetails = new ObservableCollection<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());
            }
        }

        public string _btnButtonAllColor;
        public string btnAllMonAnColor
        {
            get { return _btnButtonAllColor; }
            set
            {
                _btnButtonAllColor = value;


            }
        }

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

        private LoaiMonAn temp2;
        public LoaiMonAn selectedLoaiChiTiet
        {
            get { return temp2; }
            set
            {

                temp2 = value;
                if (temp2 != null)
                    MonAnChiTiet.MALOAI = temp2.MALOAI;

                OnPropertyChanged("selectedLoaiChiTiet");

            }
        }



        public void showDetails()
        {
            MonAnChiTiet = new MonAn();
            if (selectItem_Menu != null)
            {
                MonAnChiTiet.TENMON = selectItem_Menu.TENMON;
                MonAnChiTiet.MALOAI = selectItem_Menu.MALOAI;
                MonAnChiTiet.MAMON = selectItem_Menu.MAMON;
                MonAnChiTiet.GIA = selectItem_Menu.GIA;
                MonAnChiTiet.MOTA = selectItem_Menu.MOTA;
                MonAnChiTiet.HINHANH = selectItem_Menu.HINHANH;
                //còn create date, updatedate


                List<LoaiMonAn> tempLoai = SeviceData.getLoaiMonAn(selectItem_Menu.MALOAI);

                selectedLoaiChiTiet = tempLoai[0];
            }



            ButtonVisibility = true;
            btnDelete_Cancel = "XÓA";
            btnAdd_Save = "LƯU";
        }

        //itemCombobox loaiMonAn click
        private LoaiMonAn _selectedLoai;
        public LoaiMonAn selectedLoai
        {
            get { return _selectedLoai; }
            set
            {

                //MilkteaCategories = BindingListSeviceData.getLoaiMonAn();
                _selectedLoai = value;
                //MessageBox.Show(_selectedLoai.TENLOAI);
                //MilkteaCategories = new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database

                searchMonAnStr = "";
                btnAllMonAnColor = "#002171";
                OnPropertyChanged("btnAllMonAnColor");
                showListMonAnTheoLoai();

            }
        }

        public void showListMonAnTheoLoai()
        {
            if (selectedLoai != null)
                listMonAn = new BindingList<MonAn>(SeviceData.getListMonAnLoai(selectedLoai.MALOAI));
            ButtonVisibility = false;
        }

        private void MyMessageBox(string messageBoxText)
        {
            string caption = "Notification";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        public MonAnAdminViewModel()
        {

            //khi khoi tao thi man hinh chi tiet null
            ButtonVisibility = false;
            btnAllMonAnColor = "#002171";
            OnPropertyChanged("btnAllMonAnColor");

            //DTO
            //if (MilkteaCategories != null)
            //    MilkteaCategories.Clear();
            MilkteaCategories = new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database


            // MilkteaCategories = new BindingList<LoaiMonAn>(DataProvider.ISCreated.DB.LoaiMonAns.Where(x => x.ISDEL != 1).ToArray());// get from database

            OnPropertyChanged("MilkteaCategories");

            MilkteaCategoriesDetails = new BindingList<LoaiMonAn>();
            MilkteaCategoriesDetails = new BindingList<LoaiMonAn>(SeviceData.getLoaiMonAn());


            listMonAn = SeviceData.getListMonAn();


            //command  command  command
            Add_SaveCommand = new RelayCommand<Button>((x) =>
            {
                if (string.IsNullOrEmpty(MonAnChiTiet.TENMON) ||
                string.IsNullOrEmpty(MonAnChiTiet.MALOAI.ToString()) ||
                string.IsNullOrEmpty(MonAnChiTiet.MALOAI.ToString()) ||
                               MonAnChiTiet.MALOAI == -1 ||
                string.IsNullOrEmpty(MonAnChiTiet.GIA.ToString())
                )
                    return false;
                return true;
            },
            (x) =>
            {
                if (x.Content.ToString() == "THÊM")//ADD MON ĂN
                {

                    string res = SeviceData.themMonAn(MonAnChiTiet);
                    MyMessageBox(res);//xử lí thêm vào

                    if (res.ToString() == "Thành công")
                    {
                        if (selectedLoai == null)
                            listMonAn.Add(MonAnChiTiet);
                        else if (MonAnChiTiet.MALOAI == selectedLoai.MALOAI) listMonAn.Add(MonAnChiTiet);

                        MonAnChiTiet = new MonAn();
                    }
                }
                else if (x.Content.ToString() == "LƯU")///UPDATE MÓN ĂN
				{
                    string res = SeviceData.suaMonAn(MonAnChiTiet);
                    MyMessageBox(res);

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
                    MyMessageBox(res);//xử lí thêm vào

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

            ShowAllMonAn = new RelayCommand<ComboBox>((x) => { return true; }, (x) =>
            {
                btnAllMonAnColor = "#0277bd";
                OnPropertyChanged("btnAllMonAnColor");
                OnPropertyChanged("btnAllMonAnColor");

                searchMonAnStr = "";
                selectedLoai = null;
                x.SelectedIndex = -1;
                listMonAn = SeviceData.getListMonAn();
            });

            ChooseImgMonAn = new RelayCommand<Button>((x) => { return true; }, (x) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images File(*.png;*.jpg;*.jpeg;*.bmp*)|*.png;*.jpg;*.jpeg;*.bmp*";
                //openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == true)
                {
                    MonAnChiTiet.HINHANH = File.ReadAllBytes(openFileDialog.FileName);
                }
            });
            //click vao them mon
            addMilkteaCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                ButtonVisibility = true;
                btnDelete_Cancel = "HỦY";
                btnAdd_Save = "THÊM";

                searchMonAnStr = "";
                MonAnChiTiet = new MonAn();
                MonAnChiTiet.MALOAI = -1;
                MonAnChiTiet.MOTA = "";
                var temp = DataProvider.ISCreated.DB.MonAns.ToList();
                MonAnChiTiet.MAMON = temp.Last().MAMON + 1;
                p.SelectedIndex = -1;
            });

        }
    }
}
