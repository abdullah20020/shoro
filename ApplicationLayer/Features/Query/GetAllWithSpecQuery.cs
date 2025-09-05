using DomianLayar.Entities;
using MediatR;
using DomianLayar.contract;

public class GetAllWithSpecQuery<T> : IRequest<IReadOnlyList<T>> where T : BaseClass
{
    public Ispacfiaction<T> Spec { get; }

    public GetAllWithSpecQuery(Ispacfiaction<T> spec)
    {
        Spec = spec;
    }
}