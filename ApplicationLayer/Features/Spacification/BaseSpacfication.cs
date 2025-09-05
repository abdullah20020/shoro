using DomianLayar.contract;
using DomianLayar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Spacification
{
    public class BaseSpacfication<T> : Ispacfiaction<T> where T : BaseClass
    {
        // for  where and includes
        public Expression<Func<T, bool>> cretaria { get; set; }

        public List<Expression<Func<T, object>>> includes { get; set; } = new List<Expression<Func<T, object>>>();

        public List<Func<IQueryable<T>, IQueryable<T>>> ThenIncludes { get; set; } = new List<Func<IQueryable<T>, IQueryable<T>>>();

        // for orderby
        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderBydecync { get; set; }
        // for pigation
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Ispigation { get; set; }


        public BaseSpacfication()
        {

        }
        public BaseSpacfication(Expression<Func<T, bool>> cretaria)
        {

            this.cretaria = cretaria;
        }
        public IEnumerable<Func<IQueryable<T>, IQueryable<T>>> GetThenIncludes()
        {
            return ThenIncludes;
        }
        public void AddThenInclude(Func<IQueryable<T>, IQueryable<T>> thenInclude)
        {
            ThenIncludes.Add(thenInclude);
        }


        public void Orderby(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        public void Orderbyacync(Expression<Func<T, object>> OrderBydecync)
        {
            this.OrderBydecync = OrderBydecync;
        }
        public void ApplyPigation(int skip, int take)
        {
            this.PageIndex = skip;
            this.PageSize = take;
            this.Ispigation = true;
        }
    }
}
