using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
	class DataProvider
	{
		private static DataProvider _ISCreated;
		public static DataProvider ISCreated
		{
			get
			{
				if (_ISCreated == null)
					_ISCreated = new DataProvider();
				return _ISCreated;
			}
			set
			{
				_ISCreated = value;
			}
		}
		public QUAN_LY_QUAN_CAPHEEntities DB { get; set; }
		 private DataProvider()
		{
			DB = new QUAN_LY_QUAN_CAPHEEntities();
		}
	}
}
