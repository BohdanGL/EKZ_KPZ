using EKZ_KPZ.CQRS.Patients.Models;
using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using EKZ_KPZ.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Patients.Queries
{
    public class GetPatientById : IRequest<PatientModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetPatientById, PatientModel>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<PatientModel> Handle(GetPatientById request, CancellationToken cancellationToken)
            {
                var patient = await context.Patients
                    .AsNoTracking()
                    .Select(x => new PatientModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        OwnerSurname = x.OwnerSurname,
                        BirthDate = x.BirthDate,
                        Diagnosis = x.Diagnosis,
                    })
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (patient is null)
                {
                    throw new NotFoundException(nameof(Patient), request.Id);
                }

                return patient;
            }
        }
    }
}
