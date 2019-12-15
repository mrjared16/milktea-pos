using QuanLiQuanCaPhe.Models;
using QuanLiQuanCaPhe.View;
using QuanLiQuanCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public string tendangnhap { get; set; }

        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        // mọi thứ xử lý sẽ nằm trong này
        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (p == null)
                return;

            IsLogin = false;
            if (UserName.Equals("") || Password.Equals(""))
            {
                MessageBox.Show("Bạn chưa điền đầy đủ thông tin đăng nhập!!!");
                return;
            }
            string passEncode = MD5Hash(Base64Encode(Password));
            var Account = DataProvider.ISCreated.DB.NhanViens.FirstOrDefault(x => x.TAIKHOAN == UserName);
            if (Account == null)
            {
                MessageBox.Show("Không tồn tại tài khoản");
                return;
            }
            if (Account.ISDEL == 1)
            {
                MessageBox.Show("Tài khoản đã bị xóa");
                return;
            }
            if (!Account.MATKHAU.Equals(passEncode))
            {
                MessageBox.Show("Sai mật khẩu");
                return;
            }
            IsLogin = true;
            UserService.LoadUser(Account);

            if (UserService.GetCurrentUser.CHUCVU.Equals("Admin"))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                NhanVienLayout nhanVienLayout = new NhanVienLayout();
                nhanVienLayout.Show();
            }

            UserName = "";
            Password = "";
            p.Close();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


    }
}
