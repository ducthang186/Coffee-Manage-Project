using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Bill
    {
        private int iD;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;
        private float totalPrice;
        public Bill(DataRow row)
        {
            iD = (int)row["BillId"];
            totalPrice = (float)Convert.ToDouble( row["TotalPrice"]);
            dateCheckIn = (DateTime)row["DateCheckIn"];
            var dateCheckOutTemp = row["DateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
            {
                dateCheckOut = (DateTime)dateCheckOutTemp;
            }
            status = (int)row["Status"];
        }

        public Bill(int iD, DateTime? dateCheckIn, DateTime dateCheckOut, int status, float totalPrice)
        {
            this.iD = iD;
            this.dateCheckIn = dateCheckIn;
            this.dateCheckOut = dateCheckOut;
            this.status = status;
            this.totalPrice = totalPrice;
        }

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }
    }
}
