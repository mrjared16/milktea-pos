using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _a;
        public string a
        {
            get { return _a; }
            set
            {
                _a = value;
                OnPropertyChanged("a");
            }
        }
        public MainViewModel()
        {
            a = @"D:\save.png";

        }
    }
}
