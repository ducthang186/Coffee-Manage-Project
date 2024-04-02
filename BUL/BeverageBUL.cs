using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BUL
{
    public class BeverageBUL
    {
        private static BeverageBUL connect;
        public static BeverageBUL Connect
        {
            get
            {
                if (connect == null)
                {
                    connect = new BeverageBUL();
                }
                return connect;
            }
            private set { connect = value; }
        }
        public BeverageBUL() { }

        public List<Beverage> GetBeverageByBeverageCategoryId(int beverageCategoryId)
        {
            List<Beverage> list = new List<Beverage>();
            string query = "select * from Beverage where BeverageCategoryId =" + beverageCategoryId;
            DataTable data = DataConnection.Connection.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Beverage b = new Beverage(item);
                list.Add(b);
            }
            return list;
        }

        public DataTable GetData()
        {
            string query = "select BeverageId as [ID], Name as[Tên], BeverageCategoryId as[Danh mục] , Price as [Giá bán] from Beverage";
            DataTable result = DataConnection.Connection.ExecuteQuery(query);
            return result;
        }

        public bool InsertBeverage(Beverage beverage)
        {
            string query = "INSERT INTO Beverage(BeverageCategoryId,Name,Price) VALUES (" + beverage.BeverageCategoryId + ",N\'" + beverage.Name + "\'," + beverage.Price + ")";
            int result = DataConnection.Connection.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateBeverage(Beverage beverage)
        {
            string query = "update Beverage set Name = N\'" + beverage.Name + "\' , BeverageCategoryId = " + beverage.BeverageCategoryId + ", Price = " + beverage.Price + " where BeverageId = " + beverage.BeverageId;
            int result = DataConnection.Connection.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteBeverage(int beverageId)
        {
            BillInfoBUL.Connect.DeleteBillInfoByBeverageId(beverageId);

            string query = "delete Beverage where BeverageId = " + beverageId;
            int result = DataConnection.Connection.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable SearchBeverage(string strSearch)
        {
            string query= "select * from Beverage where Name like \'%"+ strSearch+"%\'";
            DataTable result = DataConnection.Connection.ExecuteQuery(query);
            return result;
        }

        
    }
}
