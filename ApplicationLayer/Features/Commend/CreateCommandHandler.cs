using DomianLayar.contract;
using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Commands
{
    public class CreateCommandHandler<T> : IRequestHandler<CreateCommand<T>, T> where T : BaseClass
    {
        private readonly ICommandRepository<T> _commandRepo;

        public CreateCommandHandler(ICommandRepository<T> commandRepo)
        {
            _commandRepo = commandRepo;
        }

        public async Task<T> Handle(CreateCommand<T> request, CancellationToken cancellationToken)
        {
            return await _commandRepo.AddAsync(request.Entity);
        }
    }
}