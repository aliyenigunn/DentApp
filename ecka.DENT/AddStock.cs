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
    public partial class AddStock : XtraForm
    {
        public AddStock()
        {
            InitializeComponent();
        }

        EckaDentDataContext con = new EckaDentDataContext();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                decimal d = 0;
                if (!decimal.TryParse(txtMiktar.Text, out d))
                {
                    MessageBox.Show("Lütfen miktar alanını kontrol edin!");
                    return;
                }
                if (!decimal.TryParse(txtOptimum.Text, out d))
                {
                    MessageBox.Show("Lütfen optimum miktar alanını kontrol edin!");
                    return;
                }
                con = new EckaDentDataContext();
                if (!eckaContainer.isEdit)
                {
                    STOCK add = new STOCK()
                    {
                        stockCode = txtStokKodu.Text,
                        stockName = txtStokAdi.Text,
                        stockGroup = txtStokGrubu.Text,
                        barcode = txtBarkod.Text,
                        quantity = Convert.ToInt32(txtMiktar.Text),
                        optimumQuantity = Convert.ToInt32(txtOptimum.Text),
                        warehouse = cbDepo.Text
                    };
                    con.STOCKs.InsertOnSubmit(add);
                }
                else
                {
                    STOCK st = con.STOCKs.First(x => x.stockID == eckaContainer.stockID);
                    st.quantity = Convert.ToDecimal(txtMiktar.Text);
                    st.barcode = txtBarkod.Text;
                    st.optimumQuantity = Convert.ToDecimal(txtOptimum.Text);
                    st.stockCode = txtStokKodu.Text;
                    st.stockGroup = txtStokGrubu.Text;
                    st.stockName = txtStokAdi.Text;
                    st.warehouse = cbDepo.Text;
                    eckaContainer.isEdit = false;
                }
                con.SubmitChanges();
                MessageBox.Show("Kayıt işlemi tamamlandı!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata(0xB2):" + ex.Message);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            allClear();
        }
        public void allClear()
        {
            txtStokKodu.Text = "";
            txtStokAdi.Text = "";
            txtStokGrubu.Text = "";
            txtBarkod.Text = "";
            txtMiktar.Text = "";
            txtOptimum.Text = "";
            cbDepo.SelectedText = "Seçiniz";
        }

        private void AddStock_Load(object sender, EventArgs e)
        {
            if (eckaContainer.isEdit)
            {
                txtStokAdi.Text = eckaContainer.sAdi;
                txtStokKodu.Text = eckaContainer.sKodu;
                txtStokGrubu.Text = eckaContainer.sGrup;
                txtBarkod.Text = eckaContainer.sBarkod;
                txtMiktar.Text = eckaContainer.sMiktar;
                txtOptimum.Text = eckaContainer.sOptimum;
                cbDepo.Text = eckaContainer.sDepo;
            }
        }
    }
}
