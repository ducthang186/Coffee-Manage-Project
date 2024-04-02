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
    public class BeverageTableBUL
    {

        private static BeverageTableBUL connect;

        public static BeverageTableBUL Connect
        {
            get
            {
                if (connect == null)
                {
                    connect = new BeverageTableBUL();
                }
                return connect;
            }
            private set { connect = value; }
        }
        private BeverageTableBUL() { }
        public DataTable GetData()
        {
            DataTable result = DataConnection.Connection.ExecuteQuery("SELECT * FROM BeverageTable");
            return result;
        }
        public void InsertBeverageTable(string nameTableBeverage, string statusTable)
        {
            DataConnection.Connection.ExecuteNonQuery("Insert into BeverageTable(Name,Status) values (N' " + nameTableBeverage + "' ,N'" + statusTable + "')");
        }

        public void UpdateBeverageTable(string nameTableBeverage, string idTableBeverage, string statusTable)
        {
            DataConnection.Connection.ExecuteNonQuery("update BeverageTable set Name = N'" + nameTableBeverage + "', Status = N'" + statusTable + "'  where  BeverageTableId like " + idTableBeverage);
        }


        public void DeleteBeverageTable(string idTableBeverage)
        {
            // DataConnection.Connection.ExecuteNonQuery("Delete from Bill Where BeverageTableId = " + idTableBeverage);

            DataConnection.Connection.ExecuteNonQuery("delete from  BeverageTable Where BeverageTableId = " + idTableBeverage);
        }

        public List<BeverageTable> SearchBeverageTable(string input)
        {
            List<BeverageTable> list = new List<BeverageTable>();
            DataTable result = null;
            if (input == "")
            {
                MessageBox.Show("Không được để trống");
            }
            else
            {
                result = DataConnection.Connection.ExecuteQuery("Select * From BeverageTable where Name like N'%" + input + "%'");

                foreach (DataRow item in result.Rows)
                {
                    BeverageTable bc = new BeverageTable(item);
                    list.Add(bc);
                }
            }
            return list;
        }
        public List<BeverageTable> LoadTableList()
        {
            List<BeverageTable> tableList = new List<BeverageTable>();
          
            DataTable data = DataConnection.Connection.ExecuteQuery("SELECT *  FROM BeverageTable");

            foreach (DataRow item in data.Rows)
            {
                BeverageTable table = new BeverageTable(item);
                tableList.Add(table);
            }

            return tableList;
        }
        

        public List<BeverageTable> GetTableList()
        {
            List<BeverageTable> tableList = new List<BeverageTable>();

            DataTable data = DataConnection.Connection.ExecuteQuery("SELECT * FROM BeverageTable");

            foreach (DataRow item in data.Rows)
            {
                BeverageTable table = new BeverageTable(item);
                tableList.Add(table);
            }

            return tableList;
        }



    }
}
