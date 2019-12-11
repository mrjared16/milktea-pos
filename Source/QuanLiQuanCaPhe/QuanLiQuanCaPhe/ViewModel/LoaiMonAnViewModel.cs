using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;
using QuanLiQuanCaPhe.Models;
using System.IO;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class LoaiMonAnViewModel : BaseViewModel
    {
        public ICommand findLoaiMonAnCommand { get; set; }
        public ICommand addButtonCommand { get; set; }
        public ICommand cancelButtonCommand { get; set; }
        public ICommand confirmButtonCommand { get; set; }

        private bool isAddActivity = true;

        private SeviceData seviceData = new SeviceData();

        private BindingList<LoaiMonAn> _listLoaiMonAn;

        public BindingList<LoaiMonAn> listLoaiMonAn
        {
            get
            {
                return _listLoaiMonAn;
            }
            set
            {
                _listLoaiMonAn = value;
                OnPropertyChanged();
            }
        }

        private string _queryString = "";

        public string queryString
        {
            get => _queryString;
            set
            {
                _queryString = value;
                OnPropertyChanged();
            }
        }

        private string _cancelNameButton="HỦY";

        public string cancelNameButton
        {
            get
            {
                return _cancelNameButton;
            }
            set
            {
                _cancelNameButton = value;
                OnPropertyChanged();
            }
        }

        private string _confirmNameButton="THÊM";

        public string confirmNameButton
        {
            get
            {
                return _confirmNameButton;
            }
            set
            {
                _confirmNameButton = value;
                OnPropertyChanged();
            }
        }

        private LoaiMonAn _chiTietLoaiMonAn;

        public LoaiMonAn chiTietLoaiMonAn
        {
            get
            {
                return _chiTietLoaiMonAn;
            }
            set
            {
                _chiTietLoaiMonAn = value;
                OnPropertyChanged();
            }
        }

        //itemlistView click
        private LoaiMonAn item { get; set; }
        public LoaiMonAn selectItem
        {
            get { return item; }
            set
            {
                if (item != value)
                {
                    item = value;

                    showDetails();
                }
            }
        }

        public void showDetails()
        {
            chiTietLoaiMonAn = new LoaiMonAn();
            if (selectItem != null)
            {
                chiTietLoaiMonAn.MALOAI = selectItem.MALOAI;
                chiTietLoaiMonAn.TENLOAI = selectItem.TENLOAI;
                isAddActivity = false;
                cancelNameButton = "XÓA";
                confirmNameButton = "LƯU";
            }
            else
            {
                isAddActivity = true;
                cancelNameButton = "HỦY";
                confirmNameButton = "THÊM";
            }
        }

        public LoaiMonAnViewModel()
        {
            listLoaiMonAn = new BindingList<LoaiMonAn>(seviceData.danhSachLoaiMonAn());
            chiTietLoaiMonAn = new LoaiMonAn();

            //click vao them mon
            addButtonCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddActivity = true;
                cancelNameButton = "HỦY";
                confirmNameButton = "THÊM";
                chiTietLoaiMonAn = new LoaiMonAn();
            });

            //click vao nut cancel
            cancelButtonCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddActivity)
                    chiTietLoaiMonAn = new LoaiMonAn();
                else
                {
                    if (seviceData.xoaLoaiMonAn(chiTietLoaiMonAn.MALOAI))
                    {
                        listLoaiMonAn = new BindingList<LoaiMonAn>(seviceData.danhSachLoaiMonAn());
                        chiTietLoaiMonAn = new LoaiMonAn();
                        isAddActivity = true;
                        cancelNameButton = "HỦY";
                        confirmNameButton = "THÊM";
                    }
                    else
                        MessageBox.Show("Khong xoa duoc loai mon an nay :((");
                }
            });

            //click vao nut confirm
            confirmButtonCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddActivity)
                {
                    LoaiMonAn loaiMonAn = chiTietLoaiMonAn;
                    if (seviceData.themLoaiMonAn(loaiMonAn))
                        listLoaiMonAn = new BindingList<LoaiMonAn>(seviceData.danhSachLoaiMonAn());
                    else
                        MessageBox.Show("Ma mon an da ton tai :((");
                    chiTietLoaiMonAn = new LoaiMonAn();
                }
                else
                {
                    LoaiMonAn loaiMonAn = chiTietLoaiMonAn;
                    if (seviceData.suaLoaiMonAn(loaiMonAn))
                        listLoaiMonAn = new BindingList<LoaiMonAn>(seviceData.danhSachLoaiMonAn());
                    else
                        MessageBox.Show("Khong chinh sua thong tin loai mon an duoc :((");
                    showDetails();
                }
            });

            //click vao nut tim kiem
            findLoaiMonAnCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
            {
                listLoaiMonAn = new BindingList<LoaiMonAn>(seviceData.danhSachLoaiMonAn());
            });
        }
    }
}
