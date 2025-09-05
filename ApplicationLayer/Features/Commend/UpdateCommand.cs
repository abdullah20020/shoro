using DomianLayar.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Commend
{
    public class UpdateCommand<T> : IRequest<T> where T : BaseClass
    {
        public T Entity { get; set; }

        public UpdateCommand(T entity)
        {
            Entity = entity;
        }
    }
}
