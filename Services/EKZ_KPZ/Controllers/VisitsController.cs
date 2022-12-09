using EKZ_KPZ.CQRS.Visits.Commands;
using EKZ_KPZ.CQRS.Visits.Models;
using EKZ_KPZ.CQRS.Visits.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EKZ_KPZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : BaseController
    {
        public VisitsController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get all visits.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VisitModel>))]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetVisits(), CancellationToken.None));
        }

        /// <summary>
        /// Get visit by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisitModel))]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetVisitById { Id = id }, CancellationToken.None));
        }

        /// <summary>
        /// Create visit.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult> Create(CreateVisit command)
        {
            return Ok(await Mediator.Send(command, CancellationToken.None));
        }

        /// <summary>
        /// Update visit.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult> Update(UpdateVisit command)
        {
            return Ok(await Mediator.Send(command, CancellationToken.None));
        }

        /// <summary>
        /// Delete visit by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteVisit { Id = id }, CancellationToken.None);

            return NoContent();
        }
    }
}
