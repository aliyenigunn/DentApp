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
    public partial class StockListDetail : XtraForm
    {
        public StockListDetail()
        {
            InitializeComponent();
        }
        EckaDentDataContext con = new EckaDentDataContext();

        private void StockListDetail_Load(object sender, EventArgs e)
        {
            getData();
            if (eckaContainer.authority != "Administrator")
            {
                labelControl1.Visible = false;
                labelControl2.Visible = false;
                labelControl3.Visible = false;
                labelControl4.Visible = false;
                labelControl5.Visible = false;
            }

        }
        public void getData()
        {
            con = new EckaDentDataContext();
            var q = con.STOCKs.ToList();
            gridControl1.DataSource = q;
        }

        private void StockListDetail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void yazdır(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (eckaContainer.authority == "Administrator")
            {
                if (e.KeyCode == Keys.P)
                    yazdır(sender, e);
                else if (e.KeyCode == Keys.F9)
                    yeniKartOlustur(sender, e);
                else if (e.KeyCode == Keys.F10)
                    seciliKartıDüzenle(sender, e);
                else if (e.KeyCode == Keys.F11)
                    seciliKartıSil(sender, e);
                else if (e.KeyCode == Keys.F5)
                    yenile(sender, e);

            }    
        }

        private void yeniKartOlustur(object sender, EventArgs e)
        {
            AddStock add = new AddStock(); add.Show(); eckaContainer.isEdit = false; getData();
        }

        private void seciliKartıDüzenle(object sender, EventArgs e)
        {
            try
            {
                string id = gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "stockID").ToString();
                if (id != "")
                {
                    STOCK st = con.STOCKs.First(x=>x.stockID.ToString() == id);
                    eckaContainer.stockID = st.stockID;
                    eckaContainer.sKodu = st.stockCode;
                    eckaContainer.sAdi = st.stockName;
                    eckaContainer.sGrup = st.stockGroup;
                    eckaContainer.sMiktar = st.quantity.ToString();
                    eckaContainer.sBarkod = st.barcode;
                    eckaContainer.sOptimum = st.optimumQuantity.ToString();
                    eckaContainer.sDepo = st.warehouse;
                    eckaContainer.isEdit = true;
                    AddStock a = new AddStock();
                    a.ShowDialog();
                    
                }
                getData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata (0xB1): " + ex.Message);
            }
        }

        private void seciliKartıSil(object sender, EventArgs e)
        {
            try
            {
                DialogResult dia = new DialogResult();
                dia = XtraMessageBox.Show("Seçili satır kalıcı olarak siliencektir! Bu işlemin geri dönüşü yoktur. Onaylıyor musunuz?", "Silme uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dia == DialogResult.Yes)
                {
                    if (gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "stockID").ToString() != "")
                    {
                        var z = con.STOCKs.Where(x => x.stockID.ToString() == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "stockID").ToString());
                        if (z.Any())
                        {
                            STOCK t = con.STOCKs.FirstOrDefault(x => x.stockID.ToString() == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "stockID").ToString());
                            con.STOCKs.DeleteOnSubmit(t);
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
        private void yenile(object sender, EventArgs e)
        {
            getData();
        }
    }
}
