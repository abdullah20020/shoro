using DomianLayar.contract;
using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Queries
{
    public class GetByIdQueryHandler<T> : IRequestHandler<GetByIdQuery<T>, T> where T : BaseClass
    {
        private readonly IQueryRepository<T> _queryRepo;

        public GetByIdQueryHandler(IQueryRepository<T> queryRepo)
        {
            _queryRepo = queryRepo;
        }

        public async Task<T> Handle(GetByIdQuery<T> request, CancellationToken cancellationToken)
        {
            return await _queryRepo.GetByIdAsync(request.Id);
        }
    }
}
