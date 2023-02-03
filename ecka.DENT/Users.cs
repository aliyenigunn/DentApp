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
    public partial class Users : XtraForm
    {
        public Users()
        {
            InitializeComponent();
        }
        EckaDentDataContext con = new EckaDentDataContext();
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
                yazdır(sender, e);
            else if (e.KeyCode == Keys.F9)
                yeniKullanıcıOlustur(sender, e);
            else if (e.KeyCode == Keys.F10)
                seciliKullanıcıyıDüzenle(sender, e);
            else if (e.KeyCode == Keys.F11)
                seciliKullanıcıyıSil(sender, e);
            else if (e.KeyCode == Keys.F5)
                yenile(sender, e);
        }

        private void yeniKullanıcıOlustur(object sender, EventArgs e)
        {
            AddUser add = new AddUser(); add.ShowDialog();
            eckaContainer.isEdit = false;
            getData();
        }

        
        private void seciliKullanıcıyıDüzenle(object sender, EventArgs e)
        {
           try
            {
                if (gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "userID").ToString() != "")
                {
                    
                    USER t = con.USERs.FirstOrDefault(x => x.userID.ToString() == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "userID").ToString());
                    eckaContainer.uKullaniciAdi = t.username;
                    eckaContainer.uSifre = t.password;
                    eckaContainer.uDepartman = t.department;
                    eckaContainer.uYetki = t.authority;
                    eckaContainer.editID = t.userID;
                    eckaContainer.isEdit = true;

                    AddUser newadd = new AddUser(); newadd.Show();
                }
                getData();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void seciliKullanıcıyıSil(object sender, EventArgs e)
        {
            try
            {
                DialogResult dia = new DialogResult();
                dia = XtraMessageBox.Show("Seçili satır kalıcı olarak siliencektir! Bu işlemin geri dönüşü yoktur. Onaylıyor musunuz?", "Silme uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dia == DialogResult.Yes)
                {
                    if (gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "userID").ToString() != "")
                    {
                        var z = con.USERs.Where(x => x.userID.ToString() == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "userID").ToString());
                        if (z.Count() < 2)
                        {
                            USER t = con.USERs.FirstOrDefault(x => x.userID.ToString() == gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "userID").ToString());
                            con.USERs.DeleteOnSubmit(t);
                            con.SubmitChanges();
                            XtraMessageBox.Show("Satır silindi!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void yazdır(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            con = new EckaDentDataContext();
            var q = con.USERs.ToList();
            gridControl1.DataSource = q;
        }
    }
}
