﻿using System;
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
    public class NhanVienViewModel: BaseViewModel {
        public ICommand findNhanVienCommand { get; set; }
        public ICommand confirmButtonCommmand { get; set; }
        public ICommand cancelButtonCommmand { get; set; }
        public ICommand addNhanVienCommand { get; set; }
        
        public ICommand ChonAnhCommand { get; set; }
        BitmapImage temp;

        private bool isAddActivity = true;

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

        private string _cancelButtonName="HỦY";

        public string cancelButtonName {
            get => _cancelButtonName;
            set
            {
                _cancelButtonName = value;
                OnPropertyChanged();
            }
        }

        private string _confirmButtonName = "THÊM";

        public string confirmButtonName
        {
            get => _confirmButtonName;
            set
            {
                _confirmButtonName = value;
                OnPropertyChanged();
            }
        }

        private BindingList<NhanVien> _listNhanVien;
      
        public BindingList<NhanVien> listNhanVien
        {
            get
            {
                return _listNhanVien;
            }
            set
            {
                _listNhanVien = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage DisplayedImagePath
        {
            get { return temp; }
            set { temp = value; OnPropertyChanged(); }
        }

        public ImageSource MyPhoto { get; set; }

        #region Binding
        private NhanVien _ChiTietNhanVien;
        
        public NhanVien ChiTietNhanVien
        {
            get
            {
                return _ChiTietNhanVien;
            }
            set
            {
                _ChiTietNhanVien = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //itemlistView click
        private NhanVien item { get; set; }
        public NhanVien selectItem
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
            ChiTietNhanVien = new NhanVien();
            if (selectItem != null)
            {
                ChiTietNhanVien.MANV = selectItem.MANV;
                ChiTietNhanVien.HOTEN = selectItem.HOTEN;
                ChiTietNhanVien.LUONG = selectItem.LUONG;
                ChiTietNhanVien.NGSINH = selectItem.NGSINH;
                ChiTietNhanVien.PHAI = selectItem.PHAI;
                ChiTietNhanVien.CMND = selectItem.CMND;
                ChiTietNhanVien.DIENTHOAI = selectItem.DIENTHOAI;
                ChiTietNhanVien.CHUCVU = selectItem.CHUCVU;
                isAddActivity = false;
                cancelButtonName = "XÓA";
                confirmButtonName = "LƯU";
            }
            else
            {
                isAddActivity = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
            }           
            // DisplayedImagePath = selectItem.HINHANH;
        }

        public NhanVienViewModel()
        {
            ChiTietNhanVien = new NhanVien();

            SeviceData seviceData = new SeviceData();

            ChonAnhCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                   // temp = fileUri.ToString();
                    DisplayedImagePath = temp;
                }
            }
            );

            //click vao them mon
            addNhanVienCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                isAddActivity = true;
                cancelButtonName = "HỦY";
                confirmButtonName = "THÊM";
                ChiTietNhanVien = new NhanVien();
                /*NhanVien nhanvien = ChiTietNhanVien;
                if (seviceData.themNhanVien(nhanvien))
                {
                    ChiTietNhanVien = new NhanVien();
                    listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
                }
                else
                {
                    MessageBox.Show("Ma nhan vien da ton tai :((");
                }*/
            });

            //click vao nut cancel
            cancelButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
             {
                if (isAddActivity)
                     ChiTietNhanVien = new NhanVien();
                else
                 {
                     if (seviceData.xoaNhanVien(ChiTietNhanVien.MANV))
                     { 
                         listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
                         ChiTietNhanVien = new NhanVien();
                         isAddActivity = true;
                         cancelButtonName = "HỦY";
                         confirmButtonName = "THÊM";
                     }
                     else
                         MessageBox.Show("Khong xoa duoc nhan vien nay :((");
                 }
             });

            //click vao nut confirm
            confirmButtonCommmand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (isAddActivity)
                {
                    NhanVien nhanvien = ChiTietNhanVien;
                    if (seviceData.themNhanVien(nhanvien))                
                        listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());                    
                    else                    
                        MessageBox.Show("Ma nhan vien da ton tai :((");                    
                    ChiTietNhanVien = new NhanVien();
                }
                else
                {
                    NhanVien nhanvien = ChiTietNhanVien;
                    if (seviceData.suaNhanVien(nhanvien))
                        listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
                    else
                        MessageBox.Show("Khong chinh sua thong tin nhan vien duoc :((");
                    showDetails();
                }                   
            });

            //click vao nut tim kiem
            findNhanVienCommand = new RelayCommand<Object>((P) => { return true; }, (p) =>
              {
                  listNhanVien = new BindingList<NhanVien>(seviceData.timKiemNhanVien(queryString));
              });

            listNhanVien = new BindingList<NhanVien>(seviceData.danhsachNhanVien());
        }
    }   
}