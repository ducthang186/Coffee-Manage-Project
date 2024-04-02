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
    public class BillBUL
    {
        private static BillBUL connect;

        public static BillBUL Connect
        {
            get
            {
                if (connect == null)
                {
                    connect = new BillBUL();
                }
                return connect;
            }
            private set { connect = value; }
        }
        private BillBUL() { }

        public DataTable GetData()
        {
            string query = "select t.Name, b.TotalPrice , b.DateCheckIn, b.DateCheckOut from BeverageTable t, Bill b where b.BeverageTableId = t.BeverageTableId and b.Status = 1 order by t.Name";
            DataTable result = DataConnection.Connection.ExecuteQuery(query);
            return result;
        }

        public DataTable GetBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            string query = "sp_GetBillByDate @DateCheckIn , @DateCheckOut";
            DataTable result = DataConnection.Connection.ExecuteQuery(query, new object[] {dateCheckIn,dateCheckOut });
            return result;
        }

        public int GetUncheckByBeverageTableID(int id)
        {
            DataTable data = DataConnection.Connection.ExecuteQuery("select * from Bill where BeverageTableId = " + id + " and Status = 0");

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        public void CheckOut(int billId, float totalPrice)
        {
            string query = "update Bill set Status = 1, DateCheckOut = GETDATE(), TotalPrice = " + totalPrice + " where BillId = " + billId;
            DataConnection.Connection.ExecuteNonQuery(query);

        }

        public void InsertBill(int beverageTableId)
        {
            DataConnection.Connection.ExecuteNonQuery(" sp_InsertBill @BeverageTableId", new object[] { beverageTableId });
        }
        public int GetMaxBillId()
        {
            try
            {
                return (int)DataConnection.Connection.ExecuteScalar("select max(BillId) from Bill");
            }
            catch (Exception)
            {

                return 1;
            }
        }
    }
}
