using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.View;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class NhanVienLayoutViewModelInterface : BaseViewModel
    {
        public virtual string Title { get; set; }
    }
    public class NhanVienLayoutViewModel : BaseViewModel
    {
        #region commands
        public ICommand LoadHomeView { get; set; }
        public ICommand LoadOrderView { get; set; }
        public ICommand LoadHistoryView { get; set; }
        public ICommand LoadLogoutView { get; set; }
        #endregion

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                Title = ((NhanVienLayoutViewModelInterface)value).Title;
                OnPropertyChanged(ref _currentView, value);
            }
        }



        private OrderViewModel _orderVM = new OrderViewModel();
        private OrderViewModel OrderVM
        {
            get
            {
                return _orderVM;
            }
            set { OnPropertyChanged(ref _orderVM, value); }
        }

        private HistoryViewModel _historyVM = new HistoryViewModel();
        private HistoryViewModel HistoryVM
        {
            get
            {
                return _historyVM;
            }
            set { OnPropertyChanged(ref _historyVM, value, null); }
        }

        private HomeNhanVienViewModel _taikhoanVM = new HomeNhanVienViewModel();
        private HomeNhanVienViewModel TaikhoanVM
        {
            get
            {
                return _taikhoanVM;
            }
            set { OnPropertyChanged(ref _taikhoanVM, value, null); }
        }
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnPropertyChanged(ref _Title, value);
            }
        }
        public NhanVienLayoutViewModel()
        {
            CurrentView = TaikhoanVM;
            LoadOrderView = new RelayCommand<object>((p) => { return CurrentView != OrderVM; }, (p) => { CurrentView = OrderVM; });
            LoadHistoryView = new RelayCommand<object>((p) => { return CurrentView != HistoryVM; }, (p) => { CurrentView = HistoryVM; });
            LoadHomeView = new RelayCommand<object>((p) => { return CurrentView != TaikhoanVM; }, (p) => { CurrentView = TaikhoanVM; });
            LoadLogoutView = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //MessageBox.Show(p.ToString());
                Login login = new Login();
                login.Show();
                ((Window)p).Close();
            });


            //{
            //    
            //});
        }
    }
}
