using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;
namespace BUL
{
    public class BeverageCategoryBUL
    {
        private static BeverageCategoryBUL connect;

        public static BeverageCategoryBUL Connect
        {
            get
            {
                if (connect == null)
                {
                    connect = new BeverageCategoryBUL();
                }
                return connect;
            }
            private set { connect = value; }
        }

        public bool InsertBeverageCategory(string nameBeverage)
        {
            int result = DataConnection.Connection.ExecuteNonQuery("Insert into BeverageCategory(Name) values (N' " + nameBeverage + " ')");
            return result > 0;
        }

        public bool UpdateBeverageCategory(string nameBeverage, string beverageId)
        {
            int result = DataConnection.Connection.ExecuteNonQuery("update BeverageCategory set Name = N'" + nameBeverage + "'  where  BeverageCategoryId = " + int.Parse(beverageId));
            return result > 0;
        }


        public void DeleteBeverageCategory(int beverageId)
        {
           // DataConnection.Connection.ExecuteNonQuery("delete from  Beverage  where  BeverageCategoryId = " + beverageId);
            DataConnection.Connection.ExecuteNonQuery("delete from  BeverageCategory  where  BeverageCategoryId like " + beverageId);
        }
        public BeverageCategoryBUL() { }
        public List<BeverageCategory> GetListBeverageCategory()
        {
            List<BeverageCategory> list = new List<BeverageCategory>();

            string query = "select * from BeverageCategory";
            DataTable data = DataConnection.Connection.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                BeverageCategory bc = new BeverageCategory(item);
                list.Add(bc);
            }

            return list;
        }

        public DataTable GetData()
        {
            DataTable result = DataConnection.Connection.ExecuteQuery("select BeverageCategoryId as[ID], Name as[Tên] from BeverageCategory");
            return result;
        }

        public BeverageCategory GetCategoryById(int categoryId)
        {
            string query = "select * from BeverageCategory where BeverageCategoryId = " + categoryId;
            DataTable data = DataConnection.Connection.ExecuteQuery(query);
            BeverageCategory result = new BeverageCategory(data.Rows[0]);
            return result;
        }
        public DataTable SearchBeverageCategory(string input)
        {
            DataTable result = DataConnection.Connection.ExecuteQuery("Select BeverageCategoryId as[ID], Name as[Tên] From BeverageCategory where Name like N'%" + input + "%'");
            return result;
        }
    }
}
