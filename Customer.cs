using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otomasyon_Psikolog_FİNAL
{

    [Table("Table_Customer_D")]
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_Telephone { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        //Customer sınıfının birden fazla Appointment ile işi olabileceği için Icollection olarak ekledik.



    }
}
