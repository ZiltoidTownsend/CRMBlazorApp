using Application.Features;
using Application.Features.Contacts.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Entity
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : BaseApiController<Contact>
    {
        private IMediator _mediator;
        public ContactsController(IMediator mediator) : base(mediator)
        {
        }

        protected override BaseGetAllQuery<Contact> GetAllQuery => new GetAllContactsQuery();

    }
}
