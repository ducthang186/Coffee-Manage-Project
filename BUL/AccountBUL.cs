using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUL
{
    public class AccountBUL
    {
        private static AccountBUL connect;

        public static AccountBUL Connect
        {
            get {
                if (connect == null)
                {
                    connect = new AccountBUL();
                }
                return connect;
            }
            private set { connect = value; }
        }
        private AccountBUL() { }
        public  bool Login(string username, string password)
        {
            string query = "sp_Login @UserName , @PassWord";
            DataTable result = DataConnection.Connection.ExecuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }
    }
}
