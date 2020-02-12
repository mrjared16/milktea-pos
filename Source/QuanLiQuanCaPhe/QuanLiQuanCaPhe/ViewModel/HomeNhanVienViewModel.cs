using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class HomeNhanVienViewModel : NhanVienLayoutViewModelInterface
    {
        #region commands
        public ICommand LoadProfileView { get; set; }
        public ICommand LoadScheduleView { get; set; }
        #endregion
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                OnPropertyChanged(ref _currentView, value);
            }
        }

        private TaiKhoanNhanVienViewModel _ProfileVM = null;
        private TaiKhoanNhanVienViewModel ProfileVM
        {
            get
            {
                if (_ProfileVM == null)
                {
                    _ProfileVM = new TaiKhoanNhanVienViewModel();
                }
                return _ProfileVM;
            }
            set { OnPropertyChanged(ref _ProfileVM, value); }
        }

        private LichLamViecNhanVienViewModel _ScheduleVM = null;
        private LichLamViecNhanVienViewModel ScheduleVM
        {
            get
            {
                if (_ScheduleVM == null)
                {
                    _ScheduleVM = new LichLamViecNhanVienViewModel();
                }
                return _ScheduleVM;
            }
            set { OnPropertyChanged(ref _ScheduleVM, value, null); }
        }
        public HomeNhanVienViewModel()
        {
            Title = "Trang cá nhân";
            CurrentView = ProfileVM;
            LoadProfileView = new RelayCommand<object>((p) => { return CurrentView != ProfileVM; }, (p) => { CurrentView = ProfileVM; });
            LoadScheduleView = new RelayCommand<object>((p) => { return CurrentView != ScheduleVM; }, (p) => { CurrentView = ScheduleVM; });

        }

    }
}
