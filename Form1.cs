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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        PsikologDbContext db = new PsikologDbContext();

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var customers = db.Customers.ToList();
                dataGridView1.DataSource = db.Customers.ToList();

                dataGridView1.Columns["Customer_Id"].HeaderText = "Danışan ID";
                dataGridView1.Columns["Customer_Name"].HeaderText = "Danışan Adı";
                dataGridView1.Columns["Customer_Surname"].HeaderText = "Danışan Soyadı";
                dataGridView1.Columns["Customer_Email"].HeaderText = "E-Posta";
                dataGridView1.Columns["Customer_Telephone"].HeaderText = "Telefon";

                //danışan Id kısmını gizlemek için bu kod satırını yazdık 
                dataGridView1.Columns["Customer_Id"].Visible = false;
                dataGridView1.Columns["Appointments"].Visible = false;

                //sütun genişliğini sağladık
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                dataGridView1.EnableHeadersVisualStyles = false;


                // ===== GÖRSEL TASARIM =====
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                    new Font("Segoe UI", 10, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font =
                    new Font("Segoe UI", 10, FontStyle.Regular);

                dataGridView1.DefaultCellStyle.ForeColor = Color.Teal;

                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

                dataGridView1.BackgroundColor = Color.White;
                dataGridView1.BorderStyle = BorderStyle.None;

                dataGridView1.RowHeadersVisible = false;

                dataGridView1.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.MultiSelect = false;
                dataGridView1.ReadOnly = true;
            }
            catch (Exception ex)
            { 
                MessageBox.Show($"Hata = {ex.Message}");
            }
            
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer()
                {
                    Customer_Name = txt_isim.Text,
                    Customer_Surname = txt_soyisim.Text,
                    Customer_Email = txt_mail.Text,
                    Customer_Telephone = txt_telefon.Text

                };
                db.Customers.Add(newCustomer); // Listeye ekle
                db.SaveChanges();             // Veritabanına kaydet

                MessageBox.Show("Danışan başarıyla eklendi.");
                btn_listele.PerformClick(); // güncel listeyi göster

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e harfi tıklanan hücreyi temsil eder. güncelle işleminin ilk aşaması yapıldı.
            {
                txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Name"].Value.ToString();
                txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Surname"].Value.ToString();
                txt_mail.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Email"].Value.ToString();
                txt_telefon.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Telephone"].Value.ToString();
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)// burada özellikle if kullanmamızın sebebi güncelleme butonunun mantığında öncelikle bir hücre seçilmesi gerektiğinden dolayıdır. eğer hücre seçilmezse güncelleme işlemi yapılamaz ve hata verir. bu yüzden if kullanarak hücre seçilmediği durumlarda kullanıcıya bilgi vermek istedik.
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                    //seçilen id'ye sahip müşteriyi veritabanından bul. bu kısım özellikle güncelleme noktasında bütün satırların değil yalnızca seçilen satırın güncellenmesi için önemli.
                    Customer customer = db.Customers.Find(selectedId); //find metodu ile seçilen id'ye sahip müşteriyi bulduk ve customerToUpdate değişkenine atadık. bu değişken üzerinden güncelleme işlemi yapacağız.
                    if (customer != null)
                    {
                        customer.Customer_Name = txt_isim.Text;
                        customer.Customer_Surname = txt_soyisim.Text;
                        customer.Customer_Email = txt_mail.Text;
                        customer.Customer_Telephone = txt_telefon.Text;

                        db.SaveChanges();
                        MessageBox.Show("Müşteri güncellendi!");
                        btn_listele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {

               MessageBox.Show($"Hata = {ex.Message}");

            }

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen silmek için bir danışan seçiniz.");
                    return;
                }

                int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);

                bool randevusuVarMi = db.Appointments.Any(a => a.Customer_Id == selectedId);

                if (randevusuVarMi)
                {
                    MessageBox.Show("Bu danışana ait randevu olduğu için önce randevu kaydını silmelisiniz.");
                    return;
                }

                DialogResult sonuc = MessageBox.Show(
                    "Silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    Customer customer = db.Customers.Find(selectedId);

                    if (customer != null)
                    {
                        db.Customers.Remove(customer);
                        db.SaveChanges();

                        MessageBox.Show("Danışan silindi!");
                        btn_listele.PerformClick();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_form_appointment_Click(object sender, EventArgs e)
        {
            Form_Appointment form2_cp = new Form_Appointment();
            this.Hide();
            form2_cp.ShowDialog();
            this.Show();
        }

        
    }
    
}

