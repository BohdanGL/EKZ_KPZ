using System;

namespace EKZ_KPZ.Data.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
