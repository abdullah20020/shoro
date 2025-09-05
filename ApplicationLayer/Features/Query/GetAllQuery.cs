using DomianLayar.Entities;
using MediatR;
using System.Collections.Generic;

namespace ApplicationLayer.Features.Queries
{
    public class GetAllQuery<T> : IRequest<IReadOnlyList<T>> where T : BaseClass
    {
    }
}