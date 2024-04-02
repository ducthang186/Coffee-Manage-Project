using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BeverageTable
    {
        private int iD;
        public string status;
        private string name;
        public BeverageTable() { }
        public BeverageTable(int iD, string status, string name)
        {
            this.iD = iD;
            this.status = status;
            this.name = name;
        }
        public BeverageTable(DataRow row)
        {
            iD = (int)row["BeverageTableId"];
            name = (string)row["Name"];
            status = (string)row["Status"];
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
