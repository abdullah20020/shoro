using ApplicationLayer.Features.Commend;
using DomianLayar.contract;
using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Commands
{
    public class UpdateCommandHandler<T> : IRequestHandler<UpdateCommand<T>, T> where T : BaseClass
    {
        private readonly ICommandRepository<T> _commandRepo;

        public UpdateCommandHandler(ICommandRepository<T> commandRepo)
        {
            _commandRepo = commandRepo;
        }

        public async Task<T> Handle(UpdateCommand<T> request, CancellationToken cancellationToken)
        {
            return await _commandRepo.UpdateAsync(request.Entity);
        }
    }
}