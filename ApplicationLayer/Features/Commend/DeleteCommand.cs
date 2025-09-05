using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Commands
{
    public class DeleteCommand<T> : IRequest<bool> where T : BaseClass
    {
        public int Id { get; set; }

        public DeleteCommand(int id)
        {
            Id = id;
        }
    }
}