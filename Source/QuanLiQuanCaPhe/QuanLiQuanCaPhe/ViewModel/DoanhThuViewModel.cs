using QuanLiQuanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
	public class DoanhThuViewModel: BaseViewModel
	{
		public string txtDoanhThu { get; set; }
		public BindingList<MilkteaInfo> _listMilkteaInfo { get; set; }
		public int loaiDoanhThu = -1;
		public string colorBtnSP { get; set; }
		public string colorBtnTong { get; set; }

		public ICommand btnSanPhamCommand { get; set; }
		public ICommand btnTongDoanhThuCommand { get; set; }
		public ICommand btnNgayCommand { get; set; }
		public ICommand btnThangCommand { get; set; }
		public ICommand btnNamCommand { get; set; }
		public ICommand btnQuiCommand { get; set; }

		public DoanhThuViewModel()
		{
			colorBtnTong = colorBtnSP = "#2962FF";
			OnPropertyChanged("colorBtnSP");
			OnPropertyChanged("colorBtnTong");


			/////COmmand
			btnSanPhamCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{

				loaiDoanhThu = Constants.DOANHTHU_SP;
				if (loaiDoanhThu == Constants.DOANHTHU_SP)
				{
					colorBtnSP = "#1A237E";
					colorBtnTong = "#2962FF";
				}
				else
				{
					colorBtnSP = "#2962FF";
					colorBtnTong = "#1A237E";
				}
				OnPropertyChanged("colorBtnSP");
				OnPropertyChanged("colorBtnTong");
			});

			btnTongDoanhThuCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{

				loaiDoanhThu = Constants.DOANHTHU_TONG;

				if (loaiDoanhThu == Constants.DOANHTHU_TONG)
				{
					colorBtnSP = "#2962FF";
					colorBtnTong = "#1A237E";
				}
				else
				{
					colorBtnSP = "#1A237E";
					colorBtnTong = "#2962FF";
				}
				OnPropertyChanged("colorBtnSP");
				OnPropertyChanged("colorBtnTong");
			});


			btnNgayCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{
				txtDoanhThu = "Doanh thu theo ngày";
				OnPropertyChanged("txtDoanhThu");
				if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 0; i < 10; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra sua tran chau " + i.ToString();
						a.gia = 50000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);

					}
					OnPropertyChanged("_listMilkteaInfo");
					OnPropertyChanged("color");
				}
				if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 111; i < 116; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra sua tran chau " + i.ToString();
						a.gia = 50000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);
					}
					OnPropertyChanged("_listMilkteaInfo");
				}
			});

			btnThangCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{
				txtDoanhThu = "Doanh thu theo tháng";
				OnPropertyChanged("txtDoanhThu");
				if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 22; i < 30; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra sua tran chau " + i.ToString();
						a.gia = 50000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);

					}
					OnPropertyChanged("_listMilkteaInfo");
					OnPropertyChanged("color");
				}
				if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 222; i < 225; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra sua tran chau " + i.ToString();
						a.gia = 50000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);
					}
					OnPropertyChanged("_listMilkteaInfo");
				}
			});

			btnNamCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{
				txtDoanhThu = "Doanh thu theo năm";
				OnPropertyChanged("txtDoanhThu");
				if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 0; i < 3; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra sua tran chau loai " + i.ToString();
						a.gia = 50000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);

					}
					OnPropertyChanged("_listMilkteaInfo");
					OnPropertyChanged("color");
				}
				if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 1112; i < 1126; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra sua duong den loai " + i.ToString();
						a.gia = 50000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);
					}
					OnPropertyChanged("_listMilkteaInfo");
				}
			});

			btnQuiCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
			{
				txtDoanhThu = "Doanh thu theo qúy";
				OnPropertyChanged("txtDoanhThu");
				if (loaiDoanhThu == Constants.DOANHTHU_SP)// truy van CSDL de lay doanh thu san pham theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 555; i < 559; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra sua tran chau " + i.ToString();
						a.gia = 100000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);

					}
					OnPropertyChanged("_listMilkteaInfo");
					OnPropertyChanged("color");
				}
				if (loaiDoanhThu == Constants.DOANHTHU_TONG)// truy van CSDL de lay TONG DOANH THU theo ngay
				{
					_listMilkteaInfo = new BindingList<MilkteaInfo>();
					for (int i = 122; i < 130; i++)
					{
						MilkteaInfo a = new MilkteaInfo();
						a.tenMon = "Tra dao" + i.ToString();
						a.gia = 65000;
						a.SL = 20;
						a.imgUrl = "../Image/trasua.jpg";
						_listMilkteaInfo.Add(a);
					}
					OnPropertyChanged("_listMilkteaInfo");
				}
			});
			////
		}
		public class Constants
		{
			public static int DOANHTHU_SP = 1;
			public static int DOANHTHU_TONG = 2;

		}
	}
}
