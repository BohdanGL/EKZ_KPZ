using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using EKZ_KPZ.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Patients.Commands
{
    public class DeletePatient : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeletePatient>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(DeletePatient request, CancellationToken cancellationToken)
            {
                var patient = await context.Patients.FindAsync(new object[] { request.Id }, cancellationToken);

                if (patient is null)
                {
                    throw new NotFoundException(nameof(Patient), request.Id);
                }

                context.Remove(patient);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
