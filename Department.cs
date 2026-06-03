using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otomasyon_Psikolog_FİNAL
{
    [Table("Table_Department_K")] 
    public class Department
    {
        [Key]
        public int Department_Id { get; set; }

        [Required, MaxLength(50)]
        public string Department_Name { get; set; }

        public virtual ICollection <SessionType> SessionTypes { get; set;}
        
        }
}
