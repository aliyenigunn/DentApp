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
    public partial class AddUser : XtraForm
    {
        public AddUser()
        {
            InitializeComponent();
        }
        EckaDentDataContext con = new EckaDentDataContext();
        
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                con = new EckaDentDataContext();
                if (!eckaContainer.isEdit)
                {
                    USER add = new USER()
                    {
                        username = txtKullaniciAdi.Text,
                        password = txtSifre.Text,
                        department = txtDepartman.Text,
                        authority = txtYetki.Text
                    };
                    con.USERs.InsertOnSubmit(add);
                }
                else
                {
                    USER u = con.USERs.First(x => x.userID ==eckaContainer.userID);
                    u.username = txtKullaniciAdi.Text;
                    u.password = txtSifre.Text;
                    u.department = txtDepartman.Text;
                    u.authority = txtYetki.Text;
                }
                con.SubmitChanges();
                XtraMessageBox.Show("Kayıt işlemi tamamlandı!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                eckaContainer.isEdit = false;
                Close();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            allClear();
        }

        public void allClear()
        {
            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
            txtDepartman.Text = "";
            txtYetki.Text = "";
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            if (eckaContainer.isEdit == true)
            {
                txtKullaniciAdi.Text = eckaContainer.uKullaniciAdi;
                txtSifre.Text = eckaContainer.uSifre;
                txtDepartman.Text = eckaContainer.uDepartman;
                txtYetki.Text = eckaContainer.uYetki;
            }
        }
    }
}
