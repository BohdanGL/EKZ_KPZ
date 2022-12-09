using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = EKZ_KPZ.Infrastructure.Exceptions.ValidationException;

namespace EKZ_KPZ.CQRS.Visits.Commands
{
    public class CreateVisit : IRequest<int>
    {
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public int PatientId { get; set; }

        public class Validator : AbstractValidator<CreateVisit>
        {
            public Validator()
            {
                RuleFor(x => x.Date)
                    .NotEmpty();
                RuleFor(x => x.Time)
                      .NotEmpty();
            }
        }


        public class Handler : IRequestHandler<CreateVisit, int>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(CreateVisit request, CancellationToken cancellationToken)
            {
                var isPatientExists = await context.Patients.AnyAsync(r => r.Id == request.PatientId, cancellationToken);
                if (!isPatientExists)
                {
                    throw new ValidationException(nameof(request.PatientId), new[] { $"Patient with ID {request.PatientId} does not exist." });
                }

                var visit = new Visit
                {
                    Date = request.Date,
                    Time = request.Time,
                    PatientId = request.PatientId,
                };

                context.Visits.Add(visit);
                await context.SaveChangesAsync(cancellationToken);

                return visit.Id;
            }
        }
    }
}
