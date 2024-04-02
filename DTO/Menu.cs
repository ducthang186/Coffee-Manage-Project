using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Menu
    {
        private float price;
        private string beverageName;
        private int count;
        private float totalPrice;

        public Menu(float price, string beverageName, int count, float totalPrice = 0)
        {
            this.price = price;
            this.beverageName = beverageName;
            this.count = count;
            this.totalPrice = totalPrice;
        }

        public Menu(DataRow row)
        {
            price = (float)Convert.ToDouble(row["Price"].ToString());
            beverageName = (string)row["Name"];
            count = (int)row["Count"];
            totalPrice = (float)Convert.ToDouble(row["TotalPrice"].ToString());
        }

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }


        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public string BeverageName
        {
            get { return beverageName; }
            set { beverageName = value; }
        }
    }
}
