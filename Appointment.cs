using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otomasyon_Psikolog_FİNAL
{
    [Table("Table_Appointment_R")]
    public class Appointment
    {
        [Key]
        public int Appointment_Id { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Status { get; set; }

        // Yabancı Anahtarlar (Foreign Keys)
        public int Customer_Id { get; set; }
        public int SessionType_Id { get; set; }

        //ilişki tanımlamalarımızı da buraya ekliyoruz (Navigation Properties)
        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("SessionType_Id")]
        public virtual SessionType SessionType { get; set; }
    }
}
