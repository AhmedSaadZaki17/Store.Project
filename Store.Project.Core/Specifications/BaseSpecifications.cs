using Store.Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Core.Specifications
{
    public class BaseSpecifications<TEnity, TKey> : ISpecifications<TEnity, TKey> where TEnity : BaseEntity<TKey>
    {
        public Expression<Func<TEnity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEnity, object>>> Includes { get; set; } = new List<Expression<Func<TEnity, object>>>();
        public Expression<Func<TEnity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEnity, object>> OrderByDescending { get; set; } = null;
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPaginationEnabled { get ; set; }

        public BaseSpecifications(Expression<Func<TEnity, bool>> expression)
        {
            Criteria = expression;
        }

        public BaseSpecifications()
        {
            
        }
        public void AddOrderBy(Expression<Func<TEnity, object>> expression)
        {
            OrderBy = expression;
        }
        public void AddOrderByDescinding(Expression<Func<TEnity, object>> expression)
        {
            OrderByDescending = expression;
        }
        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Take = take;
            Skip = skip;

        }

    }
}
