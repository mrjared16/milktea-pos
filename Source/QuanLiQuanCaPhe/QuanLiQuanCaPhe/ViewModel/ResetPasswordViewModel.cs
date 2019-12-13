using QuanLiQuanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiQuanCaPhe.ViewModel
{
	class ResetPasswordViewModel : BaseViewModel
	{
		private string _OldPassword;
		public string OldPassword
		{
			get => _OldPassword;
			set
			{
				_OldPassword = value;
				OnPropertyChanged();
			}
		}
		private string _NewPassword;
		public string NewPassword
		{
			get => _NewPassword;
			set
			{
				_NewPassword = value;
				OnPropertyChanged();
			}
		}
		public ICommand CloseResetPasswordCommand { get; set; }
		public ICommand ResetPasswordCommand { get; set; }
		public ResetPasswordViewModel()
		{
			ResetPasswordCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ResetPassword(p); });
			CloseResetPasswordCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
		}

		void ResetPassword(Window p)
		{
			if (MD5Hash(Base64Encode(OldPassword)).Equals(UserService.GetCurrentUser.MATKHAU))
			{
				try
				{
					NhanVien nhanVien = UserService.GetCurrentUser;
					nhanVien.MATKHAU = MD5Hash(Base64Encode(NewPassword));
					SeviceData sevice = new SeviceData();
					sevice.suaNhanVien(nhanVien);
					MessageBox.Show("Thay đổi mật khẩu thành công");
					p.Close();
				}
				catch
				{
					MessageBox.Show("Thay đổi mật khẩu thất bại");
				}

			}
			else
			{
				MessageBox.Show("Mật khẩu cũ không đúng");
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
