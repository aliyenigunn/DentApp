using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ecka.DENT
{
    public partial class StockList : XtraForm
    {
        public StockList()
        {
            InitializeComponent();
        }

        private void StockList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        EckaDentDataContext con = new EckaDentDataContext();
        private void StockList_Load(object sender, EventArgs e)
        {
            con = new EckaDentDataContext();
            var q = con.STOCKs.ToList();
            gridControl1.DataSource = q;
          

        }

        public void getData()
        {
            try
            {
                if(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "stockName").ToString() != "")
                {
                    eckaContainer.stockID = Convert.ToInt32(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "stockID"));
                    eckaContainer.stockName = gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "stockName").ToString();
                    eckaContainer.quantity = Convert.ToDecimal(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "quantity"));
                    eckaContainer.warehouse = gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "warehouse").ToString();
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            getData();
        }
    }
}
