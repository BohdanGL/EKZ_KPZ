using EKZ_KPZ.CQRS.Patients.Models;
using EKZ_KPZ.CQRS.Visits.Models;
using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using EKZ_KPZ.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Visits.Queries
{
    public class GetVisitById : IRequest<VisitModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetVisitById, VisitModel>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<VisitModel> Handle(GetVisitById request, CancellationToken cancellationToken)
            {
                var visit = await context.Visits
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
                    })
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (visit is null)
                {
                    throw new NotFoundException(nameof(Visit), request.Id);
                }

                return visit;
            }
        }
    }
}
