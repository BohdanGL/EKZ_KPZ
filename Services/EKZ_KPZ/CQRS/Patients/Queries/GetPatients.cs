using EKZ_KPZ.CQRS.Patients.Models;
using EKZ_KPZ.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Patients.Queries
{
    public class GetPatients : IRequest<IEnumerable<PatientModel>>
    {
        public class Handler : IRequestHandler<GetPatients, IEnumerable<PatientModel>>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<PatientModel>> Handle(GetPatients request, CancellationToken cancellationToken)
            {
                var patients = context.Patients
                    .AsNoTracking()
                    .Select(x => new PatientModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        OwnerSurname = x.OwnerSurname,
                        BirthDate = x.BirthDate,
                        Diagnosis = x.Diagnosis,
                    });

                return patients;
            }
        }
    }
}
