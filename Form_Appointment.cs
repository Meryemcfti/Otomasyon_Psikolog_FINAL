using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.Entity;



namespace Otomasyon_Psikolog_FİNAL
{
    public partial class Form_Appointment : Form
    {
        public Form_Appointment()
        {
            InitializeComponent();
        }

        PsikologDbContext db = new PsikologDbContext();

       
          private void btn_list_Click(object sender, EventArgs e)
        {
            try
            {
                // SessionType kısımlarını senin projenin tanıdığı gibi SessionType_S olarak güncelledik
                var list = db.Appointments
                     .Include(cp => cp.Customer)
                    .Include(cp => cp.SessionType.Department)
                    .Select(cp => new
                    {
                        Randevu_No = cp.Appointment_Id,
                        Musteri_Adi = cp.Customer.Customer_Name,
                        Musteri_Soyadi = cp.Customer.Customer_Surname,
                        Seans_Adi = cp.SessionType.SessionType_Name,
                        Ucret = cp.SessionType.SessionType_Price,
                        Departman = cp.SessionType.Department.Department_Name,
                        Durum = cp.Appointment_Status,
                        Tarih = cp.Appointment_Date
                    })
                    .ToList();

                dataGridView1.DataSource = list;
                // Tasarım ayarları
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                    new Font("Segoe UI", 10, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font =
                    new Font("Segoe UI", 10);

                dataGridView1.DefaultCellStyle.SelectionBackColor =
                    Color.LightSeaGreen;

                dataGridView1.DefaultCellStyle.SelectionForeColor =
                    Color.White;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["Randevu_No"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void Form_Appointment_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. DANIŞAN KUTUSUNU DOLDURMA
                cmb_customer.DataSource = db.Customers
                    .OrderBy(c => c.Customer_Name) // OrderBy metodu ile müşterinin adına göre alfabetik şekilde sıralıyoruz
                    .Select(c => new
                    {
                        c.Customer_Id,
                        FullName = c.Customer_Name + " " + c.Customer_Surname
                    })
                    .ToList();

                cmb_customer.DisplayMember = "FullName";            
                cmb_customer.ValueMember = "Customer_Id"; 


                // 2. SEANS TÜRÜ KUTUSUNU DOLDURMA
                cmb_sessionType.DataSource = db.SessionTypes
                    .Include(p => p.Department) // Category yerine Department tablonu dahil ettik
                    .OrderBy(p => p.SessionType_Name) // Ürün adı yerine Seans adına göre sıraladık
                    .Select(p => new
                    {
                        p.SessionType_Id,
                                          // "Çocuk Terapisi - 1500₺ (Çocuk Psikolojisi)" şeklinde şık bir görünüm oluşturduk
                        SessionTypeInfo = p.SessionType_Name + " - " + p.SessionType_Price + "₺ (" + p.Department.Department_Name + ")"
                    }).ToList();

                cmb_sessionType.DisplayMember = "SessionTypeInfo";
                cmb_sessionType.ValueMember = "SessionType_Id";

                // 3. SAAT COMBOBOX'INI DOLDURMA
                cmbSaat.Items.Clear();
                cmbSaat.Items.Add("09:00");
                cmbSaat.Items.Add("10:00");
                cmbSaat.Items.Add("11:00");
                cmbSaat.Items.Add("12:00");
                cmbSaat.Items.Add("13:00");
                cmbSaat.Items.Add("14:00");
                cmbSaat.Items.Add("15:00");
                cmbSaat.Items.Add("16:00");
                cmbSaat.Items.Add("17:00");

                cmbSaat.SelectedIndex = 0;


                // 3. LİSTEYİ OTOMATİK YENİLEME
                // Form açıldığında randevular DataGridView'e direkt gelsin diye Listeleme metodunu çağırıyoruz.
                // btn_list_Click(sender, e); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                // ComboBox'lardan seçilen ID'lerini alıyoruz (Senin formundaki isimlerine göre: cmb_customer, cmb_session)
                int selectedCustomerId = (int)cmb_customer.SelectedValue;
                int selectedSessionId = (int)cmb_sessionType.SelectedValue;

                // Yeni CustomerProduct yerine senin projenin karşılığı olan Appointment (Randevu) nesnesi oluşturuyoruz
                var newRecord = new Appointment
                {
                    Customer_Id = selectedCustomerId,
                    SessionType_Id = selectedSessionId,

                    // Not: Senin Randevu tablonda Tarih ve Durum sütunları da olduğu için onları da buraya ekledim.
                    Appointment_Date = dateTimePicker1.Value.Date + TimeSpan.Parse(cmbSaat.Text),
                   
                    Appointment_Status = "AKTİF"
                };

                // CustomerProducts tablosu yerine senin db.Appointments tablosuna yeni kaydımızı ekliyoruz
                db.Appointments.Add(newRecord);
                db.SaveChanges();

                MessageBox.Show("Kayıt başarıyla eklendi.");

                // Listeleme işleminin tanımlı olduğu metodu çalıştırıyoruz (Senin listeleme metodunun adına göre güncellendi)
                btn_list_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show(
                "Silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {


                    if (dataGridView1.CurrentRow != null)
                    {
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Randevu_No"].Value);
                        //seçilen id'ye sahip randevuyu veritabanından bul. bu kısım özellikle silme noktasında bütün satırların değil yalnızca seçilen satırın silinmesi için önemli.
                        Appointment appointmentToDelete = db.Appointments.Find(selectedId); //find metodu ile seçilen id'ye sahip randevuyu bulduk ve appointmentToDelete değişkenine atadık. bu değişken üzerinden silme işlemi yapacağız.
                        if (appointmentToDelete != null)
                        {
                            db.Appointments.Remove(appointmentToDelete);
                            db.SaveChanges();
                            MessageBox.Show("Randevu silindi!");
                            btn_list_Click(sender, e); // Listeleme metodunu çağırarak DataGridView'i güncelliyoruz.
                        }
                        else
                        {
                            MessageBox.Show("Seçilen randevu bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void rd_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (rd_1.Checked)
                {
                    var result = db.Appointments
                        .GroupBy(cp =>new
                        {
                            cp.Customer.Customer_Id,
                            cp.Customer.Customer_Name,
                            cp.Customer.Customer_Surname
                        })
                        .Select(g => new
                        {
                            Musteri_Adi = g.Key.Customer_Name,
                            Musteri_Soyadi = g.Key.Customer_Surname,
                            Toplam_Randevu = g.Count()
                        })

                        .OrderByDescending(x => x.Toplam_Randevu) // Toplam randevu sayısına göre azalan şekilde sıralama yapıyoruz. En çok randevuya sahip müşteriyi alıyoruz
                        .FirstOrDefault();

                    if(result != null)
                    {
                        lbl_1.Text=($" {result.Musteri_Adi} {result.Musteri_Soyadi} - Toplam Randevu: {result.Toplam_Randevu}");
                    }
                    else
                    {
                        lbl_1.Text = ("Hiç randevu bulunamadı.");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void rd_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_2.Checked)
                {
                    var result = db.Appointments
                        .Include(cp => cp.SessionType)
                        .GroupBy(cp => new
                        {
                            cp.SessionType.SessionType_Id,
                            cp.SessionType.SessionType_Name
                        })
                        .Select(g => new
                        {
                            Seans_Adi = g.Key.SessionType_Name,
                            Toplam_Satis = g.Count()
                        })
                        .OrderByDescending(x => x.Toplam_Satis) // Toplam satış sayısına göre azalan şekilde sıralama yapıyoruz. En çok satılan seans türünü alıyoruz
                        .FirstOrDefault(); 
                    if (result != null)
                    {
                        lbl_2.Text = ($" {result.Seans_Adi} - Toplam Satış: {result.Toplam_Satis}");
                    }
                    else
                    {
                        lbl_2.Text = ("Hiç randevu bulunamadı.");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_form_customer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
    
}
