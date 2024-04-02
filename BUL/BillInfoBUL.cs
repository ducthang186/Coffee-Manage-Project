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
    public class BillInfoBUL
    {
        private static BillInfoBUL connect;
        public static BillInfoBUL Connect
        {
            get
            {
                if (connect == null)
                {
                    connect = new BillInfoBUL();
                }
                return connect;
            }
            private set { connect = value; }
        }
        public DataTable GetData()
        {
            DataTable result = DataConnection.Connection.ExecuteQuery("SELECT * FROM BillInfo");
            return result;
        }

        public List<BillInfor> GetListBillInforByBillId(int BillId)
        {
            List<BillInfor> result = new List<BillInfor>();
            DataTable data = DataConnection.Connection.ExecuteQuery("select * from BillInfo where BillId = " + BillId);

            foreach (DataRow item in data.Rows)
            {
                BillInfor bi = new BillInfor(item);
                result.Add(bi);
            }

            return result;
        }

        public void InsertBillInfor(int BillId, int BeverageId, int count)
        {
            DataConnection.Connection.ExecuteNonQuery(" sp_InsertBillInfor @BillId , @BeverageId , @count", new object[] { BillId, BeverageId, count });
        }

        public void DeleteBillInfoByBeverageId( int beverageId)
        {
            DataConnection.Connection.ExecuteNonQuery("delete from BillInfo where BeverageId = "+ beverageId);
        }
    }
}
