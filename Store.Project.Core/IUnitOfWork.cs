using Store.Project.Core.Entities;
using Store.Project.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

        // create Repository<T> and return
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
