using Store.Project.Core;
using Store.Project.Core.Entities;
using Store.Project.Core.Repositories.Contract;
using Store.Project.Repository.Data.Contexts;
using Store.Project.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repository;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repository = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
           if (! _repository.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity, TKey>(_context);
                _repository.Add(type, repository);
            }
            return _repository[type] as IGenericRepository<TEntity, TKey>;
        }
    }
}
