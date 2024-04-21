using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ContactsRepository : RepositoryAsync<Contact, Guid>
{
    public ContactsRepository(MainContext dbContext) : base(dbContext)
    {
    }
}
