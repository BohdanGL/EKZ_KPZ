using EKZ_KPZ.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EKZ_KPZ.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
    }
}
