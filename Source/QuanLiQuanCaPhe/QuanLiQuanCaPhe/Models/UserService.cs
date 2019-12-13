using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
    public class UserService
    {
        private static NhanVien _CurrentUser = null;
        public static NhanVien GetCurrentUser
        {
            get
            {
                return _CurrentUser;
            }
        }

        public static void LoadUser(NhanVien user)
        {
            _CurrentUser = user;
        }
    }
}
