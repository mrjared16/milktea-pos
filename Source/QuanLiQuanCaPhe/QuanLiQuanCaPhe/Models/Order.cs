using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCaPhe.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public Category(LoaiMonAn a)
        {
            this.Name = a.TENLOAI;
            this.ID = a.MALOAI;
        }
        public Category()
        {
        }
    }
    public class Drink
    {
        private double price;
        private string name;
        private byte[] img;
        private string _ID;
        public Drink(string name, float price, string img_source)
        {
            this.price = price;
            this.name = name;
            this.img = null;
        }
        public Drink(MonAn monan)
        {
            this.price = Convert.ToDouble(monan.GIA);
            this.name = monan.TENMON;
            this.img = monan.HINHANH;
            this.ID = monan.MAMON;
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Name
        {
            get { return name; }
        }
        public string Label
        {
            get { return (name.Length > 15) ? name.Substring(0, 12) + "..." : name; }
        }
        public double Price
        {
            get { return price; }
        }
        public byte[] Image
        {
            get { return img; }
        }
    }

}
