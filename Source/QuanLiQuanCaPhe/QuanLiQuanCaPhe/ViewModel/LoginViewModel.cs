using QuanLiQuanCaPhe.Models;
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


            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();


            //LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            //CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (p == null)
                return;

            string passEncode = MD5Hash(Base64Encode(Password));
            var accCount = DataProvider.ISCreated.DB.NhanViens.Where(x => x.TAIKHOAN == UserName && x.MATKHAU == passEncode);

            if (UserName.Equals("") || Password.Equals(""))
            {
                MessageBox.Show("Bạn chưa điền đầy đủ thông tin đăng nhập!!!");
            }
            else if (accCount.Count() > 0)
            {
                foreach (var item in accCount)
                {
                    if (item.ISDEL == 1)
                    {
                        FileStream fileStream = new FileStream("tumeo.txt", FileMode.Create, FileAccess.ReadWrite);
                        byte[] temp = Encoding.UTF8.GetBytes(UserName);
                        fileStream.Write(temp, 0, temp.Length);
                        fileStream.Close();
                        IsLogin = true;
                        tendangnhap = UserName;

                        if (item.CHUCVU.Equals("Admin"))
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                        }
                        else
                        {
                            NhanVienMainWindow nhanVienMainWindow = new NhanVienMainWindow();
                            nhanVienMainWindow.Show();
                        }
                        UserName = "";
                        Password = "";
                        p.Close();
                    }
                }
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
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
