using DomianLayar.Entities;
using MediatR;

namespace ApplicationLayer.Features.Queries
{
    public class GetByIdQuery<T> : IRequest<T> where T : BaseClass
    {
        public int Id { get; set; }

        public GetByIdQuery(int id)
        {
            Id = id;
        }
    }
}