using Application.Features;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Contexts;
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
        private BaseGetAllQuery<T> _getAllQuery;

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected MainContext MainContext { get; set; }
        public BaseApiController(IMediator mediator, BaseGetAllQuery<T> getAllQuery)
        {
            _mediatorInstance = mediator;
            _getAllQuery = getAllQuery;
        }

        /// <summary>
        /// Get All Entities
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetAll(int skipCount = 0, int getCount = 0, string? sortingString = "")
        {
            _getAllQuery.SkipCount = skipCount;
            _getAllQuery.GetCount = getCount;
            _getAllQuery.SortingString = sortingString;


            var result = await _mediator.Send(_getAllQuery);
                   
            return Ok(result);
        }
    }
}
