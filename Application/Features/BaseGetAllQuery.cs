using Application.Interfaces.Repositories;
using CRMBlazorApp.Shared.Wrapper;
using Domain.Contracts;
using MediatR;

namespace Application.Features;
public abstract class BaseGetAllQuery<TResponse> : IRequest<IEnumerable<TResponse>> where TResponse : AuditableEntity<Guid>
{

}

public abstract class BaseGetAllQueryHandler<TResponse> : IRequestHandler<BaseGetAllQuery<TResponse>, IEnumerable<TResponse>> where TResponse : AuditableEntity<Guid>
{
    private readonly IUnitOfWork<Guid> _unitOfWork;
    public BaseGetAllQueryHandler(IUnitOfWork<Guid> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TResponse>> Handle(BaseGetAllQuery<TResponse> command, CancellationToken cancellationToken)
    {
        var resultData = await _unitOfWork.Repository<TResponse>().GetAllAsync();

        return resultData;      
    }
}

