using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Commands
{
    public class CreateCommand<T> : IRequest<T> where T : BaseClass
    {
        public T Entity { get; set; }

        public CreateCommand(T entity)
        {
            Entity = entity;
        }
    }
}