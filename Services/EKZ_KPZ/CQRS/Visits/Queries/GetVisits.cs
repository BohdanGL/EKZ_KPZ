using EKZ_KPZ.CQRS.Patients.Models;
using EKZ_KPZ.CQRS.Visits.Models;
using EKZ_KPZ.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Visits.Queries
{
    public class GetVisits : IRequest<IEnumerable<VisitModel>>
    {
        public class Handler : IRequestHandler<GetVisits, IEnumerable<VisitModel>>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<VisitModel>> Handle(GetVisits request, CancellationToken cancellationToken)
            {
                var visits = context.Visits
                    .AsNoTracking()
                    .Select(x => new VisitModel
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Time = x.Time,
                        Patient = new PatientModel
                        {
                            Id = x.Patient.Id,
                            Name = x.Patient.Name,
                            Type = x.Patient.Type,
                            OwnerSurname = x.Patient.OwnerSurname,
                            BirthDate = x.Patient.BirthDate,
                            Diagnosis = x.Patient.Diagnosis,
                        }
                    });

                return visits;
            }
        }
    }
}
