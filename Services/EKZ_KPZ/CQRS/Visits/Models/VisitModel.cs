using EKZ_KPZ.CQRS.Patients.Models;
using System;

namespace EKZ_KPZ.CQRS.Visits.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public  PatientModel Patient { get; set; }
    }
}
