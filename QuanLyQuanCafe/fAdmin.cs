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
using CrystalDecisions.Shared;
using DTO;
namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        private string statusTable = "";
        private string statusCategoty = "";
        private string statusBeverage = "";
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }
       private void Load()
        {
            LoadBill();
            LoadDefaultBeverage();
            LoadBeverage();
            LoadCategoryCombobox(cbCategory);
            FormDefaultCategory();
            LoadBeverageCategory();
            LoadTable();
        }
        private void fAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        void LoadBeverage()
        {
            dgvBeverage.DataSource = BeverageBUL.Connect.GetData();
        }
        void LoadBeverageCategory()
        {
            dgvBeverageCategory.DataSource = BeverageCategoryBUL.Connect.GetData() ;
        }

        void FormDefaultCategory()
        {
            txtBeverageCategoryId.Text = "";
            txtBeverageCategoryName.Text = "";
            txtSearchBeverageCategoryName.Text = "";

            txtBeverageCategoryId.Enabled = false;
            txtBeverageCategoryName.Enabled = false;

            btnDeleteBeverageCategory.Enabled = false;
            btnEditBeverageCategory.Enabled = false;
            btnSaveBeverageCategory.Enabled = false;
            btnCancelBeverageCategory.Enabled = false;
            btnAddBeverageCategory.Enabled = true;
        }

        void LoadDefaultBeverage()
        {
            txtBeverageId.Text = "";
            txtBeverageName.Text = "";
            txtBeverageId.Enabled = false;
            txtBeverageName.Enabled = false;

            btnCancelBeverage.Enabled = false;
            btnDeleteBeverage.Enabled = false;
            btnEditBeverage.Enabled = false;
            btnSaveBeverage.Enabled = false;
            btnAddBeverage.Enabled = true;
            nmBeveragePrice.Value = 0;

        }

        void LoadCategoryCombobox(ComboBox cb)
        {
            cb.DataSource = BeverageCategoryBUL.Connect.GetListBeverageCategory();
            cb.DisplayMember = "Name";
        }

        void LoadBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
            dgvBill.DataSource = BillBUL.Connect.GetBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        void LoadBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            if (DateTime.Compare(dateCheckIn, dateCheckOut) <= 0)
            {
                DataTable result = BillBUL.Connect.GetBillByDate(dateCheckIn, dateCheckOut);
                if (result.Rows.Count == 0)
                {
                    MessageBox.Show("Không có đơn hàng nào trong khoảng thời gian này");
                }
                else
                {
                    dgvBill.DataSource = result;
                }
            }
            else
            {
                MessageBox.Show("Ngày vào phải nhỏ hơn ngày ra");
            }

        }


        #region event

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadBillByDate(DateTime.Parse(dtpkFromDate.Value.ToShortDateString()), DateTime.Parse(dtpkToDate.Value.ToShortDateString()));
        }

        private void dgvBeverage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDefaultBeverage();
            int row = e.RowIndex;
            txtBeverageId.Text = dgvBeverage[0, row].Value.ToString();
            txtBeverageName.Text = dgvBeverage[1, row].Value.ToString();
            BeverageCategory category = BeverageCategoryBUL.Connect.GetCategoryById((int)dgvBeverage[2, row].Value);
            int index = -1;
            int i = 0;
            foreach (BeverageCategory item in cbCategory.Items)
            {
                if (item.BeverageCategoryID == category.BeverageCategoryID)
                {
                    index = i;
                    break;
                }
                i++;
            }
            cbCategory.SelectedIndex = index;
            nmBeveragePrice.Value = Convert.ToInt32(dgvBeverage[3, row].Value);
            btnEditBeverage.Enabled = true;
            btnDeleteBeverage.Enabled = true;
        }

        private void btnAddBeverage_Click(object sender, EventArgs e)
        {
            LoadDefaultBeverage();
            btnCancelBeverage.Enabled = true;
            btnSaveBeverage.Enabled = true;
            txtBeverageName.Enabled = true;
            statusBeverage = "Add";

        }

        private void btnEditBeverage_Click(object sender, EventArgs e)
        {
            btnCancelBeverage.Enabled = true;
            btnSaveBeverage.Enabled = true;
            txtBeverageName.Enabled = true;
            btnAddBeverage.Enabled = false;
            statusBeverage = "Edit";
        }
        private void btnCancelBeverage_Click(object sender, EventArgs e)
        {
            LoadDefaultBeverage();
        }

        private void btnSaveBeverage_Click(object sender, EventArgs e)
        {
            if (statusBeverage == "Add")
            {
                if (txtBeverageName.Text == "")
                {
                    MessageBox.Show("Tên không được rỗng!");
                }
                else if (nmBeveragePrice.Value == 0)
                {
                    MessageBox.Show("Giá bán phải lớn hơn 0!");
                }
                else
                {
                    int categoryID = (cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID;
                    string name = txtBeverageName.Text;
                    int price = Convert.ToInt32(nmBeveragePrice.Value);
                    if (BeverageBUL.Connect.InsertBeverage(new Beverage(categoryID, name, price)))
                    {
                        MessageBox.Show("Thêm thành công");
                        LoadDefaultBeverage();
                        LoadBeverage();
                        if (insertBeverage != null)
                        {
                            insertBeverage(this, new EventArgs());
                        }

                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công, hãy kiểm tra lại");
                    }
                }
            }
            else
            {
                if (txtBeverageName.Text == "")
                {
                    MessageBox.Show("Tên không được rỗng!");
                }
                else if (nmBeveragePrice.Value == 0)
                {
                    MessageBox.Show("Giá bán phải lớn hơn 0!");
                }
                else
                {
                    int beverageID = Convert.ToInt32(txtBeverageId.Text);
                    int categoryID = (cbCategory.SelectedItem as BeverageCategory).BeverageCategoryID;
                    string name = txtBeverageName.Text;
                    int price = Convert.ToInt32(nmBeveragePrice.Value);
                    if (BeverageBUL.Connect.UpdateBeverage(new Beverage(beverageID, name, categoryID, price)))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        LoadBeverage();
                        LoadDefaultBeverage();
                        if (updateBeverage != null)
                        {
                            updateBeverage(this, new EventArgs());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công, hãy kiểm tra lại");
                    }
                }
            }
        }


        private void btnDeleteBeverage_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtBeverageId.Text);
            if (MessageBox.Show("Bạn có muốn xóa đồ uống có mã là " + id + " không ?", "Thông báo", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                if (!BeverageBUL.Connect.DeleteBeverage(id))
                {
                    MessageBox.Show("Xóa không thành công ");
                }
                else
                {
                    LoadBeverage();
                    if (deleteBeverage != null)
                    {
                        deleteBeverage(this, new EventArgs());
                    }
                }
            }
        }

        private void btnSearchBeverage_Click(object sender, EventArgs e)
        {
            string strSearch = txtSearchBeverageName.Text;
            DataTable result = BeverageBUL.Connect.SearchBeverage(strSearch);
            if (result.Rows.Count > 0)
            {
                dgvBeverage.DataSource = result;
            }
            else
            {
                MessageBox.Show("Không có đồ uốn nào chứa ký tự " + strSearch);
            }
        }

        private event EventHandler insertBeverage;
        public event EventHandler InsertBeverage
        {
            add { insertBeverage += value; }
            remove { insertBeverage -= value; }
        }

        private event EventHandler deleteBeverage;
        public event EventHandler DeleteBeverage
        {
            add { deleteBeverage += value; }
            remove { deleteBeverage -= value; }
        }

        private event EventHandler updateBeverage;
        public event EventHandler UpdateBeverage
        {
            add { updateBeverage += value; }
            remove { updateBeverage -= value; }
        }

    
        #endregion

       

        private void btnSaveBeverageCategory_Click(object sender, EventArgs e)
        {
            if (statusCategoty == "Add")
            {
                if (txtBeverageCategoryName.Text == "")
                {
                    MessageBox.Show("Tên không được rỗng");
                }
                else
                {
                    string nameBC = txtBeverageCategoryName.Text;
                    if (!BeverageCategoryBUL.Connect.InsertBeverageCategory(nameBC))
                    {
                        MessageBox.Show("Thêm không thành công");
                    }
                    else
                    {
                        LoadBeverageCategory();
                        FormDefaultCategory();
                    }
                }
            }
            else
            {
                if (txtBeverageCategoryName.Text == "")
                {
                    MessageBox.Show("Tên không được rỗng");
                }
                else
                {
                    string nameBC = txtBeverageCategoryName.Text;
                    string id = txtBeverageCategoryId.Text;
                    if (!BeverageCategoryBUL.Connect.UpdateBeverageCategory(nameBC, id))
                    {
                        MessageBox.Show("Sửa không thành công");
                    }
                    else
                    {
                        LoadBeverageCategory();
                        FormDefaultCategory();
                    }
                }
            }
        }

        private void btnEditBeverageCategory_Click(object sender, EventArgs e)
        {
            btnCancelBeverageCategory.Enabled = true;
            btnSaveBeverageCategory.Enabled = true;
            txtBeverageCategoryName.Enabled = true;
            btnAddBeverageCategory.Enabled = false;
            statusCategoty = "Edit";
        }

        private void btnCancelBeverageCategory_Click(object sender, EventArgs e)
        {
            FormDefaultCategory();
        }

        private void dgvBeverageCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FormDefaultCategory();
            int dong = e.RowIndex;
            txtBeverageCategoryId.Text = dgvBeverageCategory[0, dong].Value.ToString();
            txtBeverageCategoryName.Text = dgvBeverageCategory[1, dong].Value.ToString();
            btnEditBeverageCategory.Enabled = true;
            btnDeleteBeverageCategory.Enabled = true;
        }

        private void btnAddBeverageCategory_Click(object sender, EventArgs e)
        {
            FormDefaultCategory();
            btnCancelBeverageCategory.Enabled = true;
            btnSaveBeverageCategory.Enabled = true;
            txtBeverageCategoryName.Enabled = true;

            statusCategoty = "Add";
        }

        private void btnDeleteBeverageCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtBeverageCategoryId.Text);
            if (MessageBox.Show("Bạn có muốn xóa doanh mục có mã là " + id + " không ?", "Thông báo", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                BeverageCategoryBUL.Connect.DeleteBeverageCategory(id);
                
            }
            LoadBeverageCategory();
        }

        private void btnSearchBeverageCategory_Click(object sender, EventArgs e)
        {
            string search = txtSearchBeverageCategoryName.Text;
           DataTable result  = BeverageCategoryBUL.Connect.SearchBeverageCategory(search);
            if (result.Rows.Count >0)
            {
                dgvBeverageCategory.DataSource = result;
            }
            else
            {
                MessageBox.Show("Không có danh mục có chứa ký tự " + search);
            }
        }

        #region table

        
        public void LoadTable()
        {
            cbBeverageTableStatus.Items.Add("Trống");
            cbBeverageTableStatus.Items.Add("Có Người");
            List<BeverageTable> listCategory = BeverageTableBUL.Connect.GetTableList();
            dgvBeverageTable.DataSource = listCategory;
            FormDefaultTableBeverage();
        }

        void FormDefaultTableBeverage()
        {
            txtBeverageTableId.Text = "";
            txtBeverageTableId.Enabled = false;
            txtBeverageTableName.Text = "";
            txtBeverageTableName.Enabled = false;
            cbBeverageTableStatus.Text = "";
            cbBeverageTableStatus.Enabled = false;
            btnAddBeverageTable.Enabled = true;
            btnDeleteBeverageTable.Enabled = false;
            btnEditBeverageTable.Enabled = false;
            btnSaveBeverageTable.Enabled = false;
            btnBeverageTableCancel.Enabled = false;

        }

        //them
        void InsertBeverageTable()
        {
            BeverageTableBUL.Connect.InsertBeverageTable(txtBeverageTableName.Text, cbBeverageTableStatus.SelectedItem.ToString());
        }

        void DeleteBeverageTable()
        {
            BeverageTableBUL.Connect.DeleteBeverageTable(txtBeverageTableId.Text);
        }

        void UpdateBeverageTable()
        {
            BeverageTableBUL.Connect.UpdateBeverageTable(txtBeverageTableName.Text, txtBeverageTableId.Text, cbBeverageTableStatus.SelectedItem.ToString());
        }

        #endregion

        private void btnAddBeverageTable_Click(object sender, EventArgs e)
        {
            btnSaveBeverageTable.Enabled = true;
            txtBeverageTableName.Enabled = true;
            btnBeverageTableCancel.Enabled = true;
            txtBeverageTableId.Text = "Tự động thêm";
            cbBeverageTableStatus.Enabled = true;
            cbBeverageTableStatus.Text = "";
            txtBeverageTableName.Text = "";
            statusTable = "1";
        }

        private void btnDeleteBeverageTable_Click(object sender, EventArgs e)
        {
            statusTable = "2";
        }

        private void btnEditBeverageTable_Click(object sender, EventArgs e)
        {
            statusTable = "3";
            btnSaveBeverageTable.Enabled = true;
            btnBeverageTableCancel.Enabled = true;
            txtBeverageTableName.Enabled = true;
            cbBeverageTableStatus.Enabled = true;

        }

        private void btnSaveBeverageTable_Click(object sender, EventArgs e)
        {
            switch (statusTable)
            {
                case "1":
                    InsertBeverageTable();
                    break;
                case "2":
                    DialogResult dl = MessageBox.Show("Bạn có muốn xóa", "Xóa Bàn", MessageBoxButtons.YesNo);
                    if (dl == DialogResult.Yes)
                    {
                        DeleteBeverageTable();
                    }
                    break;
                case "3":

                    DialogResult dl1 = MessageBox.Show("Bạn có muốn sửa", "Sửa Bàn", MessageBoxButtons.YesNo);
                    if (dl1 == DialogResult.Yes)
                    {
                        UpdateBeverageTable();
                    }
                    break;



            }
            LoadTable();
        }

        private void dgvBeverageTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtBeverageTableId.Text = dgvBeverageTable[0, dong].Value.ToString();
            txtBeverageTableName.Text = dgvBeverageTable[2, dong].Value.ToString();
            cbBeverageTableStatus.Text = dgvBeverageTable[1, dong].Value.ToString();
           
            btnEditBeverageTable.Enabled = true;
            btnDeleteBeverageTable.Enabled = true;
            btnAddBeverageTable.Enabled = true;
            btnSaveBeverageTable.Enabled = true;
            btnBeverageTableCancel.Enabled = true;
        }

     

        private void txtBeverageTableName_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtBeverageTableName.Text == "")
            {
                btnAddBeverageTable.Enabled = true;
            }
            else
            {
                btnAddBeverageTable.Enabled = true;
                btnEditBeverageTable.Enabled = true;
                btnDeleteBeverageTable.Enabled = false;
            }
        }

        private void cbBeverageTableStatus_MouseClick(object sender, MouseEventArgs e)
        {
            if (cbBeverageTableStatus.Text == "")
            {
                btnAddBeverageTable.Enabled = true;
            }
            else
            {
                btnAddBeverageTable.Enabled = true;
                btnEditBeverageTable.Enabled = true;
                btnDeleteBeverageTable.Enabled = false;
            }
        }

        private void btnBeverageTableCancel_Click(object sender, EventArgs e)
        {
            FormDefaultTableBeverage();
        }

        private void btnPrintBeverage_Click(object sender, EventArgs e)
        {
            frpBeverage f = new frpBeverage();
            f.crystalReportViewer1.RefreshReport();
            f.ShowDialog();
        }

        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            ParameterValues par = new ParameterValues();
            ParameterValues par2 = new ParameterValues();
            ParameterDiscreteValue ngayvao = new ParameterDiscreteValue();
            ParameterDiscreteValue ngayra = new ParameterDiscreteValue();
            ngayvao.Value = dtpkFromDate.Value.ToString("yyyy/MM/dd");
            ngayra.Value = dtpkToDate.Value.ToString("yyyy/MM/dd");
            par.Add(ngayvao);
            par2.Add(ngayra);
            crptRevenue rpRevenue = new crptRevenue();
            //rpRevenue.SetParameterValue("@DataCheckIn",);
            rpRevenue.DataDefinition.ParameterFields["@DateCheckIn"].ApplyCurrentValues(par);
            rpRevenue.DataDefinition.ParameterFields["@DateCheckOut"].ApplyCurrentValues(par2);
            frpRevenue f = new frpRevenue();
            f.crystalReportViewer1.ReportSource = rpRevenue;
            f.crystalReportViewer1.RefreshReport();
            f.ShowDialog();
        }
    }
}
