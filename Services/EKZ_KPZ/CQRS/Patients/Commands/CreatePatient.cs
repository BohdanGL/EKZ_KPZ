using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Patients.Commands
{
    public class CreatePatient : IRequest<int>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string OwnerSurname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Diagnosis { get; set; }

        public class Validator : AbstractValidator<CreatePatient>
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


        public class Handler : IRequestHandler<CreatePatient, int>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(CreatePatient request, CancellationToken cancellationToken)
            {
                var patient = new Patient
                {
                    Name = request.Name,
                    Type = request.Type,
                    OwnerSurname = request.OwnerSurname,
                    BirthDate = request.BirthDate,
                    Diagnosis = request.Diagnosis,
                };

                context.Patients.Add(patient);
                await context.SaveChangesAsync(cancellationToken);

                return patient.Id;
            }
        }
    }
}
