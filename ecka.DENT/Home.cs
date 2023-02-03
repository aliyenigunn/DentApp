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
    public partial class Home : XtraForm
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelControl1.Text = "Bağlı olan kullanıcı : " + eckaContainer.username;
            labelControl2.Text = "Departman : " + eckaContainer.department;
            labelControl3.Text = "Yetki : " + eckaContainer.authority;

            if (eckaContainer.authority != "Administrator")
            {
                btnKullanicilar.Enabled = false;
            }
        }

        private void btnStok_Click(object sender, EventArgs e)
        {
            StockListDetail stc = new StockListDetail(); stc.ShowDialog();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            UrunCikis urn = new UrunCikis(); urn.ShowDialog();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            eckaContainer.isLogged = false;
            Application.Restart();
        }

        private void btnKullanicilar_Click(object sender, EventArgs e)
        {
            Users us = new Users(); us.ShowDialog();
        }
    }
}
