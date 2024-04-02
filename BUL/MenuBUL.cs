using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BUL
{
    public class MenuBUL
    {
        private static MenuBUL connect;

        public static MenuBUL Connect
        {
            get
            {
                if (connect == null)
                {
                    connect = new MenuBUL();
                }
                return connect;
            }

            private set { connect = value; }
        }
        private MenuBUL() { }

        public List<Menu> GetMenuListByBeverageTableID(int beverageTableID)
        {
            List<Menu> result = new List<Menu>();
            string query= "select be.Name, bi.Count, be.Price, be.Price*bi.Count as TotalPrice from Bill as b , BillInfo as bi, Beverage as be where b.BillId = bi.BillId and bi.BeverageId = be.BeverageId and b.Status =0  and b.BeverageTableId = " + beverageTableID;
            DataTable data = DataConnection.Connection.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Menu mn = new Menu(item);
                result.Add(mn);
            }
            return result;
        }
    }
}
