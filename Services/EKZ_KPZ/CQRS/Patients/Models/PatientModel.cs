using System;

namespace EKZ_KPZ.CQRS.Patients.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string OwnerSurname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Diagnosis { get; set; }
    }
}
