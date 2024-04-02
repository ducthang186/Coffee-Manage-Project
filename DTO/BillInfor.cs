using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillInfor
    {

        private int billId;
        private int beverageId;
        private int count;

        public BillInfor(DataRow row)
        {
            BillId = (int)row["BillId"];
            beverageId = (int)row["BeverageId"];
            count = (int)row["Count"];
        }
        public BillInfor(int billId, int beverageId, int count)
        {

            this.billId = billId;
            this.beverageId = beverageId;
            this.count = count;
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public int BillId
        {
            get { return billId; }
            set { billId = value; }
        }
        public int BeverageId
        {
            get { return beverageId; }
            set { beverageId = value; }
        }


    }
}
