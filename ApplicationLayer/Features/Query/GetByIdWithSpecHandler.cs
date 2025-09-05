using DomianLayar.Entities;
using ApplicationLayer.Features.Spacification;
using MediatR;
using DomianLayar.contract;

public class GetByIdWithSpecHandler<T> : IRequestHandler<GetByIdWithSpecQuery<T>, T>
    where T : BaseClass
{
    private readonly IQueryRepository<T> _queryRepo;

    public GetByIdWithSpecHandler(IQueryRepository<T> queryRepo)
    {
        _queryRepo = queryRepo;
    }

    public async Task<T> Handle(GetByIdWithSpecQuery<T> request, CancellationToken cancellationToken)
    {
        return await _queryRepo.GetByIdWithSpecificationAsync(request.Spec);
    }
}