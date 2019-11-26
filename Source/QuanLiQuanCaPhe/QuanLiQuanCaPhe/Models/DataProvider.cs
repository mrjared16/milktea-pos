using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
	class DataProvider
	{
		private static DataProvider isCreated;
		public static DataProvider ISCreated
		{
			get
			{
				if (isCreated == null)
					isCreated = new DataProvider();
				return isCreated;
			}
			set
			{
				isCreated = value;
			}
		}
		public QUAN_LY_QUAN_CAPHEEntities DB { get; set; }
		 private DataProvider()
		{
			DB = new QUAN_LY_QUAN_CAPHEEntities();
		}
	}
}
