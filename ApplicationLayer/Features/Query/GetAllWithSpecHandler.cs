using DomianLayar.Entities;
using ApplicationLayer.Features.Spacification;
using MediatR;
using DomianLayar.contract;

public class GetAllWithSpecHandler<T> : IRequestHandler<GetAllWithSpecQuery<T>, IReadOnlyList<T>>
    where T : BaseClass
{
    private readonly IQueryRepository<T> _queryRepo;

    public GetAllWithSpecHandler(IQueryRepository<T> queryRepo)
    {
        _queryRepo = queryRepo;
    }

    public async Task<IReadOnlyList<T>> Handle(GetAllWithSpecQuery<T> request, CancellationToken cancellationToken)
    {
        return await _queryRepo.GetAllWithSpecificationAsync(request.Spec);
    }
}