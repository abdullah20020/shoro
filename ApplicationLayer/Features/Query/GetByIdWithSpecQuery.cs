using DomianLayar.Entities;
using ApplicationLayer.Features.Spacification;
using MediatR;
using DomianLayar.contract;

public class GetByIdWithSpecQuery<T> : IRequest<T> where T : BaseClass
{
    public Ispacfiaction<T> Spec { get; }

    public GetByIdWithSpecQuery(Ispacfiaction<T> spec)
    {
        Spec = spec;
    }
}