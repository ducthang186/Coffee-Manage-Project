using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class BeverageCategory
    {
        private int beverageCategoryID;
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int BeverageCategoryID
        {
            get { return beverageCategoryID; }
            set { beverageCategoryID = value; }
        }

        public BeverageCategory(DataRow row)
        {
            beverageCategoryID = (int)row["BeverageCategoryId"];
            name = (string)row["Name"];
        }

        public BeverageCategory(int beverageCategoryID, string name)
        {
            this.beverageCategoryID = beverageCategoryID;
            this.name = name;
        }
    }
}
