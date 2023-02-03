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
    public partial class UrunCikis : XtraForm
    {
        public UrunCikis()
        {
            InitializeComponent();
        }
        EckaDentDataContext con = new EckaDentDataContext();
        private void UrunCikis_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            if (eckaContainer.authority != "Administrator")
                btnSil.Enabled = false;
            StockList st = new StockList(); st.ShowDialog();
            textEdit1.Text = eckaContainer.stockName;
            getData();
            textEdit1.Focus();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UrunCikis_Shown(object sender, EventArgs e)
        {
            textEdit2.Focus();
        }
        public void getData()
        {
            con = new EckaDentDataContext();
            var q = con.CIKISBILGIs.OrderByDescending(x => x.ID).ToList();
            gridControl1.DataSource = q;
        }
        public void allClear()
        {
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit3.Text = "";
        }

        private void textEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                con = new EckaDentDataContext();
                if (e.KeyCode == Keys.Enter)
                {
                    if (textEdit1.Text != "" && textEdit2.Text != "" && textEdit3.Text != "")
                    {
                        OPROCESS add = new OPROCESS()
                        {
                            stockID = eckaContainer.stockID,
                            userID = eckaContainer.userID,
                            Oquantity = Convert.ToDecimal(textEdit2.Text),
                            Odate = DateTime.Now,
                            usageArea = textEdit3.Text


                        };
                        con.OPROCESSes.InsertOnSubmit(add);
                        STOCK st = con.STOCKs.First(x => x.stockID == eckaContainer.stockID);
                        st.quantity = st.quantity - Convert.ToDecimal(textEdit2.Text);
                        con.SubmitChanges();
                        getData();
                        allClear();
                        XtraMessageBox.Show("Kayıt işlemi tamamlandı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                        XtraMessageBox.Show("Lütfen boşlukları doldurunuz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dia = new DialogResult();
                dia = XtraMessageBox.Show("Seçili satır kalıcı olarak siliencektir! Bu işlemin geri dönüşü yoktur. Onaylıyor musunuz?", "Silme uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dia == DialogResult.Yes)
                {
                    if (gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "ID").ToString() != "")
                    {
                        var z = con.OPROCESSes.Where(x => x.processID.ToString() == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "ID").ToString());
                        if (z.Any())
                        {
                            decimal silinenMiktar = Convert.ToDecimal(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "CIKISMIKTARI").ToString());
                            STOCK duz = con.STOCKs.FirstOrDefault(x => x.stockName == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "STOKADI").ToString());
                            duz.quantity += silinenMiktar;
                            con.SubmitChanges();
                            OPROCESS t = con.OPROCESSes.FirstOrDefault(x => x.processID.ToString() == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "ID").ToString());
                            con.OPROCESSes.DeleteOnSubmit(t);
                            con.SubmitChanges();
                            XtraMessageBox.Show("Satır silindi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                    }
                    else XtraMessageBox.Show("Satır seçmediniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                getData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (eckaContainer.authority == "Administrator")
            {
                if (e.KeyCode == Keys.Delete)
                    btnSil.PerformClick();
            }

        }

        private void textEdit1_Click(object sender, EventArgs e)
        {
            UrunCikis_Load(sender, e);
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            textEdit2.SelectAll();
            textEdit2.Focus();
        }
    }
}
