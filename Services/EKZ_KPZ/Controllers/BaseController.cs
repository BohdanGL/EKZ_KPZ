using EKZ_KPZ.Infrastructure.ExceptionsHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EKZ_KPZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ValidationExceptionInfo), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionInfo), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionInfo), StatusCodes.Status500InternalServerError)]
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
