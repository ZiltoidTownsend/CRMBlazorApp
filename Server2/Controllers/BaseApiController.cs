using Application.Features;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase where T : AuditableEntity<Guid>
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;

        protected virtual BaseGetAllQuery<T> GetAllQuery => throw new NotImplementedException();

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        public BaseApiController(IMediator mediator)
        {
            _mediatorInstance = mediator;
        }

        /// <summary>
        /// Get All Entities
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(GetAllQuery);
            return Ok(result);
        }
    }
}
