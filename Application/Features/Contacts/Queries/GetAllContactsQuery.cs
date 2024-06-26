﻿using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Features.Contacts.Queries;
public class GetAllContactsQueryHandler : BaseGetAllQueryHandler<Contact>
{
    public GetAllContactsQueryHandler(IUnitOfWork<Guid> unitOfWork) : base(unitOfWork)
    {
    }
}

public class GetAllContactsQuery : BaseGetAllQuery<Contact>
{
    public GetAllContactsQuery(int skipCount = 0, int getCount = 0) : base(skipCount, getCount) { }
}

