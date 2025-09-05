using DomianLayar.contract;
using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Commands
{
    public class DeleteCommandHandler<T> : IRequestHandler<DeleteCommand<T>, bool> where T : BaseClass
    {
        private readonly ICommandRepository<T> _commandRepo;
        private readonly IQueryRepository<T> _queryRepo;

        public DeleteCommandHandler(ICommandRepository<T> commandRepo, IQueryRepository<T> queryRepo)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
        }

        public async Task<bool> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
        {
            return await _commandRepo.DeleteAsync(request.Id);
        }
    }
}