using DomianLayar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayar.contract
{
    public interface IQueryRepository<T> where T : BaseClass
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(Ispacfiaction<T> spec);
        Task<T> GetByIdWithSpecificationAsync(Ispacfiaction<T> spec);
    }
}
