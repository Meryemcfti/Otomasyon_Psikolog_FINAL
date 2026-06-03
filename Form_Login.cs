using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon_Psikolog_FİNAL
{
    public partial class Form_Login : Form
    {
        private bool sifreGorunuyor = false;

        public Form_Login()
        {
            InitializeComponent();
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            txtKullaniciAdi.Text = "Kullanıcı Adı";
            txtKullaniciAdi.ForeColor = Color.Gray;

            txtSifre.Text = "Şifre";
            txtSifre.ForeColor = Color.Gray;
            txtSifre.UseSystemPasswordChar = false;
        }

        private void txtKullaniciAdi_Enter(object sender, EventArgs e)
        {

        }

        private void txtKullaniciAdi_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                txtKullaniciAdi.Text = "Kullanıcı Adı";
                txtKullaniciAdi.ForeColor = Color.Gray;
            }
        }

        private void txtSifre_Enter(object sender, EventArgs e)
        {
            if (txtSifre.Text == "Şifre")
            {
                txtSifre.Text = "";
                txtSifre.ForeColor = Color.Black;
                txtSifre.UseSystemPasswordChar = true;
            }
        }

        private void txtSifre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                txtSifre.UseSystemPasswordChar = false;
                txtSifre.Text = "Şifre";
                txtSifre.ForeColor = Color.Gray;
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;

            if (kullaniciAdi == "meryem" && sifre == "1234")
            {
                Form1 anaForm = new Form1();
                this.Hide();
                anaForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            }
        }

        private void pbGoz_Click(object sender, EventArgs e)
        {
            if (txtSifre.Text == "Şifre")
                return;

            sifreGorunuyor = !sifreGorunuyor;
            txtSifre.UseSystemPasswordChar = !sifreGorunuyor;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
