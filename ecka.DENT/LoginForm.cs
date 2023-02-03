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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dia = XtraMessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dia == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        EckaDentDataContext con = new EckaDentDataContext();

        private void LoginForm_Load(object sender, EventArgs e)
        {
            eckaContainer.isLogged = false;
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    eckaContainer.isLogged = false;
                    if (textBox1.Text != "" || textBox2.Text != "")
                    {
                        con = new EckaDentDataContext();
                        var q = con.USERs.Where(x => x.username == textBox1.Text && x.password == textBox2.Text).ToList();
                        if (q.Any())
                        {
                            foreach (var c in q)
                            {
                                USER t = con.USERs.First(x => x.userID == c.userID);
                                t.lastLogin = DateTime.Now.ToString();
                                con.SubmitChanges();
                                eckaContainer.userID = c.userID;
                                eckaContainer.username = c.username;
                                eckaContainer.department = c.department;
                                eckaContainer.authority = c.authority;
                                eckaContainer.lastLogin = c.lastLogin;
                                eckaContainer.isLogged = true;
                                break;
                            }
                            Close();
                        }
                        else
                            XtraMessageBox.Show("Kullanıcı adı veya şifre hatalı!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        XtraMessageBox.Show("Kullanıcı adı/şifre boş bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata(0xB3): " + ex.Message);
            }
        }
    }
}
