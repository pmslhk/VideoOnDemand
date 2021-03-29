using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace VOD.Database.Services
{
    public class DbReadService : IDbReadService
    {
        private VODContext _db;
        public DbReadService(VODContext db)
        {
            _db = db;
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return await _db.Set<TEntity>().AnyAsync(expression);

        }

        public async Task<List<TEntity>> GetAsync<TEntity>(
                      Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return await _db.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class
        {
            
            throw new NotImplementedException();
        }

        public void Include<TEntity>() where TEntity : class
        {
            var propertyNames = _db.Model.FindEntityType(typeof(TEntity)).
                                    GetNavigations().Select(e => e.Name);


            foreach (var name in propertyNames)
                _db.Set<TEntity>().Include(name).Load();

            //throw new NotImplementedException();
        }

        public void Include<TEntity1, TEntity2>()
            where TEntity1 : class
            where TEntity2 : class
        {
            Include<TEntity1>();
            Include<TEntity2>();

            //throw new NotImplementedException();
        }

        public async Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return await _db.Set<TEntity>().Where(expression)
 .SingleOrDefaultAsync();

            //throw new NotImplementedException();
        }


    }
}
