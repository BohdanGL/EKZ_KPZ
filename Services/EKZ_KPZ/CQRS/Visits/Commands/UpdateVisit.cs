using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using EKZ_KPZ.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = EKZ_KPZ.Infrastructure.Exceptions.ValidationException;

namespace EKZ_KPZ.CQRS.Visits.Commands
{
    public class UpdateVisit : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public int PatientId { get; set; }

        public class Validator : AbstractValidator<UpdateVisit>
        {
            public Validator()
            {
                RuleFor(x => x.Date)
                    .NotEmpty();
                RuleFor(x => x.Time)
                      .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateVisit, int>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(UpdateVisit request, CancellationToken cancellationToken)
            {
                var visit = await context.Visits.FindAsync(new object[] { request.Id }, cancellationToken);

                if (visit is null)
                {
                    throw new NotFoundException(nameof(Visit), request.Id);
                }

                var isPatientExists = await context.Patients.AnyAsync(r => r.Id == request.PatientId, cancellationToken);
                if (!isPatientExists)
                {
                    throw new ValidationException(nameof(request.PatientId), new[] { $"Patient with ID {request.PatientId} does not exist." });
                }

                visit.Date = request.Date;
                visit.Time = request.Time;
                visit.PatientId = request.PatientId;

                await context.SaveChangesAsync(cancellationToken);

                return visit.Id;
            }
        }
    }
}
