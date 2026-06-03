using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Otomasyon_Psikolog_FİNAL
{
    [Table("Table_SessionType_S")]
    public class SessionType
    {
        [Key]
        public int SessionType_Id { get; set; }

        [Required, MaxLength(50)]
        public string SessionType_Name { get; set; }

        [Required]
        public decimal SessionType_Price { get; set; }

        [ForeignKey("Department")]
        public int Department_Id { get; set; }

        public virtual Department Department { get; set; }
        
        public virtual ICollection<Appointment> Appointments { get; set; }

        
    }
}
