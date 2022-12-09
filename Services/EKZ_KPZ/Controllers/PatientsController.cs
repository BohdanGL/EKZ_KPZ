using EKZ_KPZ.CQRS.Patients.Commands;
using EKZ_KPZ.CQRS.Patients.Models;
using EKZ_KPZ.CQRS.Patients.Queries;
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
    public class PatientsController : BaseController
    {
        public PatientsController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get all patients.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PatientModel>))]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetPatients(), CancellationToken.None));
        }

        /// <summary>
        /// Get patient by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientModel))]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetPatientById { Id = id }, CancellationToken.None));
        }

        /// <summary>
        /// Create patient.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult> Create(CreatePatient command)
        {
            return Ok(await Mediator.Send(command, CancellationToken.None));
        }

        /// <summary>
        /// Update patient.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult> Update(UpdatePatient command)
        {
            return Ok(await Mediator.Send(command, CancellationToken.None));
        }

        /// <summary>
        /// Delete patient by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePatient { Id = id }, CancellationToken.None);

            return NoContent();
        }
    }
}
