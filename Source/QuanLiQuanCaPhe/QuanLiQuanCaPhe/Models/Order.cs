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
    }
    public class Drink
    {
        private float price;
        private string name;
        private string img_source;

        public Drink(string name, float price, string img_source)
        {
            this.price = price;
            this.name = name;
            this.img_source = img_source;
        }

        public string Name
        {
            get { return name; }
        }
        public float Price
        {
            get { return price; }
        }
        public string ImgSource()
        {
            return img_source;
        }
    }

}
