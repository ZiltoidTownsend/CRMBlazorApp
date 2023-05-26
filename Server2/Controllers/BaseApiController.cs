using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();

        /// <summary>
        /// Get All Entities
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var brands = await _mediator.Send(new GetAllBaseQuery());
            return Ok(brands);
        }
    }
}
