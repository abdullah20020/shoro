using DomianLayar.contract;
using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Queries
{
    public class GetAllQueryHandler<T> : IRequestHandler<GetAllQuery<T>, IReadOnlyList<T>> where T : BaseClass
    {
        private readonly IQueryRepository<T> _queryRepo;

        public GetAllQueryHandler(IQueryRepository<T> queryRepo)
        {
            _queryRepo = queryRepo;
        }

        public async Task<IReadOnlyList<T>> Handle(GetAllQuery<T> request, CancellationToken cancellationToken)
        {
            return await _queryRepo.GetAllAsync();
        }
    }
}