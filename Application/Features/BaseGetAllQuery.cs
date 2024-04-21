using Application.Interfaces.Repositories;
using CRMBlazorApp.Shared.Wrapper;
using Domain.Contracts;
using MediatR;

namespace Application.Features;
public abstract class BaseGetAllQuery<TResponse> : IRequest<IEnumerable<TResponse>> where TResponse : AuditableEntity<Guid>
{
    public int SkipCount { get; set; }
    public int GetCount { get; set; }
    public string SortingString { get; set; }
    public BaseGetAllQuery(int skipCount = 0, int getCount = 0)
    {
        SkipCount = skipCount;
        GetCount = getCount;
    }

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
        var resultData = await _unitOfWork.Repository<TResponse>().GetAllAsync(command.SkipCount, command.GetCount, command.SortingString);

        return resultData;      
    }
}

