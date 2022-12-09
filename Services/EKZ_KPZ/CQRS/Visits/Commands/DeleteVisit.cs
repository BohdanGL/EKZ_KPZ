using EKZ_KPZ.Data;
using EKZ_KPZ.Data.Models;
using EKZ_KPZ.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.CQRS.Visits.Commands
{
    public class DeleteVisit : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteVisit>
        {
            private readonly AppDbContext context;

            public Handler(AppDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(DeleteVisit request, CancellationToken cancellationToken)
            {
                var visit = await context.Visits.FindAsync(new object[] { request.Id }, cancellationToken);

                if (visit is null)
                {
                    throw new NotFoundException(nameof(Visit), request.Id);
                }

                context.Remove(visit);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
