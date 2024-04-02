using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Beverage
    {
        private int beverageId;
        private string name;
        private int beverageCategoryId;
        private float price;

        public Beverage(int beverageId, string name, int beverageCategoryId, float price)
        {
            this.beverageId = beverageId;
            this.name = name;
            this.beverageCategoryId = beverageCategoryId;
            this.price = price;
        }

        public Beverage( int beverageCategoryId, string name, float price)
        {
            this.name = name;
            this.beverageCategoryId = beverageCategoryId;
            this.price = price;
        }

        public Beverage(DataRow row)
        {
            beverageId = (int)row["BeverageId"];
            name = (string)row["Name"];
            beverageCategoryId = (int)row["BeverageCategoryId"];
            price = (float)Convert.ToDouble(row["Price"].ToString());
        }


        public int BeverageId
        {
            get { return beverageId; }
            set { BeverageId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int BeverageCategoryId
        {
            get { return beverageCategoryId; }
            set { beverageCategoryId = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
