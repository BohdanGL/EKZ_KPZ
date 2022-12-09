using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using EKZ_KPZ.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Patients.Commands
{
    public class UpdatePatient : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string OwnerSurname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Diagnosis { get; set; }

        public class Validator : AbstractValidator<UpdatePatient>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .Length(2, 50);
                RuleFor(x => x.Type)
                    .NotEmpty()
                    .Length(2, 50);
                RuleFor(x => x.OwnerSurname)
                    .NotEmpty()
                    .Length(2, 50);
                RuleFor(x => x.Diagnosis)
                    .NotEmpty()
                    .Length(2, 50);
            }
        }

        public class Handler : IRequestHandler<UpdatePatient, int>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(UpdatePatient request, CancellationToken cancellationToken)
            {
                var patient = await context.Patients.FindAsync(new object[] { request.Id }, cancellationToken);

                if (patient is null)
                {
                    throw new NotFoundException(nameof(Patient), request.Id);
                }

                patient.Name = request.Name;
                patient.Type = request.Type;
                patient.OwnerSurname = request.OwnerSurname;
                patient.BirthDate = request.BirthDate;
                patient.Diagnosis = request.Diagnosis;

                await context.SaveChangesAsync(cancellationToken);

                return patient.Id;
            }
        }
    }
}
