using Microsoft.EntityFrameworkCore;
using Store.Project.Core.Entities;
using Store.Project.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Repository
{
    public class SpecificationsEvaluator<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = InputQuery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPaginationEnabled)
            {
              query =  query.Skip(spec.Skip).Take(spec.Take);
            }


            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


            return query;
        }

       // _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
    }
}
