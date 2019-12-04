using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using QuanLiQuanCaPhe.Models;

namespace QuanLiQuanCaPhe.ViewModels
{
    public class NhanVienLayoutViewModelInterface : BaseViewModel
    {
        public virtual string Title { get; set; }
    }
    public class NhanVienLayoutViewModel : BaseViewModel
    {
        #region commands
        public ICommand HomeView { get; set; }
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
        private OrderViewModel _orderVM = null;
        private OrderViewModel OrderVM
        {
            get
            {
                if (_orderVM == null)
                {
                    _orderVM = new OrderViewModel();
                }
                return _orderVM;
            }
            set { OnPropertyChanged(ref _orderVM, value); }
        }

        private HistoryViewModel _historyVM = null;
        private HistoryViewModel HistoryVM
        {
            get
            {
                if (_historyVM == null)
                {
                    _historyVM = new HistoryViewModel();
                }
                return _historyVM;
            }
            set { OnPropertyChanged(ref _historyVM, value); }
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
            CurrentView = OrderVM;
            LoadOrderView = new RelayCommand<object>((p) => { return CurrentView != OrderVM; }, (p) => { CurrentView = OrderVM;  /*var a = DataProvider.ISCreated.DB.NhanViens.First(); */});
            LoadHistoryView = new RelayCommand<object>((p) => { return CurrentView != HistoryVM; }, (p) => { CurrentView = HistoryVM; });
            //{
            //    
            //});
        }
    }
}
