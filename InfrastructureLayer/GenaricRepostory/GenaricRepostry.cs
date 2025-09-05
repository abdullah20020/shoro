using ApplicationLayer.Features.Spacification;
using DomianLayar.contract;
using DomianLayar.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.GenaricRepostory
{
public class GenaricRepostry<T> : ICommandRepository<T>, IQueryRepository<T> where T : BaseClass
{
    private readonly ShoraDbContext _context;

    public GenaricRepostry(ShoraDbContext context)
    {
        _context = context;
    }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
     
        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(Ispacfiaction<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
       
        public async Task<T> GetByIdWithSpecificationAsync(Ispacfiaction<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
       
        private IQueryable<T> ApplySpecification(Ispacfiaction<T> spec)
        {
            return spacificationEvalator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
