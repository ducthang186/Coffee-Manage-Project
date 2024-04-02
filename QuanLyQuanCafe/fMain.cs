using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUL;
using DTO;
using Menu = DTO.Menu;

namespace QuanLyQuanCafe
{
    public partial class fMain : Form
    {

        public fMain()
        {
            InitializeComponent();
            LoadBeverageTable();
            LoadBeverageCategory();
        }

        void ShowBill(int beverageTableId)
        {

            float totalPrice = 0;
            lsvBill.Items.Clear();
            List<Menu> menuList = MenuBUL.Connect.GetMenuListByBeverageTableID(beverageTableId);
            foreach (Menu item in menuList)
            {
                ListViewItem lsvItem = new ListViewItem(item.BeverageName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(string.Format("{0:#,0.####}", item.Price));
                lsvItem.SubItems.Add(string.Format("{0:#,0.####}", item.TotalPrice));
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            txtTotalPrice.Text = string.Format("{0:#,0.####}", totalPrice);

        }

        void LoadBeverageCategory()
        {
            List<BeverageCategory> listCategory = BeverageCategoryBUL.Connect.GetListBeverageCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadBeverageByBeverageCategoryID(int beverageCategoryId)
        {
            List<Beverage> listCategory = BeverageBUL.Connect.GetBeverageByBeverageCategoryId(beverageCategoryId);
            cbBeverage.DataSource = listCategory;
            cbBeverage.DisplayMember = "Name";
        }

        void LoadBeverageTable()
        {
            flpBeverageTable.Controls.Clear();
            List<BeverageTable> tableList = BeverageTableBUL.Connect.LoadTableList();
            foreach (BeverageTable item in tableList)
            {
                Button btn = new Button() { Width = 100, Height = 100 };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btnTable_click;
                btn.Tag = item;
                switch (item.status)
                {
                    case "Trống":
                        btn.BackColor = Color.LawnGreen;
                        break;
                    default:
                        btn.BackColor = Color.OrangeRed;
                        break;
                }
                flpBeverageTable.Controls.Add(btn);
                flpBeverageTable.BringToFront();
            }


        }

        #region Events
        void btnTable_click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as BeverageTable).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableId);


        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fLogin f = new fLogin();
            fAdmin fa = new fAdmin();
            fa.InsertBeverage += Fa_InsertBeverage;
            fa.UpdateBeverage += Fa_UpdateBeverage;
            fa.DeleteBeverage += Fa_DeleteBeverage;
            fa.Show();
        }

        private void Fa_DeleteBeverage(object sender, EventArgs e)
        {
            LoadBeverageByBeverageCategoryID((cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID);
            if (lsvBill.Tag != null)
            {

                ShowBill((lsvBill.Tag as BeverageTable).ID);
            }
            LoadBeverageByBeverageCategoryID((cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID);
            LoadBeverageTable();
        }

        void Fa_UpdateBeverage(object sender, EventArgs e)
        {
            LoadBeverageByBeverageCategoryID((cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID);
            if (lsvBill.Tag != null)
            {

                ShowBill((lsvBill.Tag as BeverageTable).ID);
            }
            LoadBeverageByBeverageCategoryID((cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID);
        }

        void Fa_InsertBeverage(object sender, EventArgs e)
        {
            LoadBeverageByBeverageCategoryID((cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID);
            if (lsvBill.Tag != null)
            {

                ShowBill((lsvBill.Tag as BeverageTable).ID);
            }
            LoadBeverageByBeverageCategoryID((cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID);

        }

        void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            int status = 0;
            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
                status = 1;
            }
            if (status == 0)
            {
                e.Cancel = true;
            }

        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            BeverageCategory selected = cb.SelectedItem as BeverageCategory;

            LoadBeverageByBeverageCategoryID(selected.BeverageCategoryID);
        }

        private void btnAddBeverage_Click(object sender, EventArgs e)
        {
            BeverageTable table = lsvBill.Tag as BeverageTable;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int billId = BillBUL.Connect.GetUncheckByBeverageTableID(table.ID);
            int beverageId = (cbBeverage.SelectedItem as Beverage).BeverageId;
            int count = (int)nmBeverageCount.Value;
            if (billId == -1)
            {
                BillBUL.Connect.InsertBill(table.ID);
                BillInfoBUL.Connect.InsertBillInfor(BillBUL.Connect.GetMaxBillId(), beverageId, count);
            }
            else
            {
                BillInfoBUL.Connect.InsertBillInfor(billId, beverageId, count);
            }
            ShowBill(table.ID);
            LoadBeverageTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            BeverageTable table = lsvBill.Tag as BeverageTable;
            int billId = BillBUL.Connect.GetUncheckByBeverageTableID(table.ID);
            float totalPrice = (float)Convert.ToDouble(txtTotalPrice.Text);
            if (billId != -1)
            {
                if (MessageBox.Show("Bạn có chắc thanh toán hóa đơn cho bàn " + table.Name + " không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillBUL.Connect.CheckOut(billId, totalPrice);
                    ShowBill(table.ID);
                    LoadBeverageTable();
                }

            }
        }

        #endregion
    }
}
