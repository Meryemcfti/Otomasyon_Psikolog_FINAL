using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Otomasyon_Psikolog_FİNAL
{
    public class PsikologDbContext : DbContext // 
    {
        public PsikologDbContext() : base("name=PsikologDbContext")
        {

        }
        public DbSet<Customer> Customers { get; set; } // sql'deki customer tablomuzu temsil eder.

        public DbSet<SessionType> SessionTypes { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
