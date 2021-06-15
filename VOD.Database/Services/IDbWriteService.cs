using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;

namespace VOD.Database.Services
{
    public interface IDbWriteService
    {
        Task<bool> SaveChangesAsync();
        void Add<TEntity>(TEntity item) where TEntity : class;
        void Delete<TEntity>(TEntity item) where TEntity : class;
        void Update<TEntity>(TEntity item) where TEntity : class;
    }

    public class DbWriteService : IDbWriteService
    {
        private readonly VODContext _db;
        public DbWriteService(VODContext db)
        {
            _db = db;
        }

        public void Add<TEntity>(TEntity item) where TEntity : class
        {
            try
            {
                _db.Set<TEntity>().Add(item);
            }
            catch
            {
                throw;
            }

        }

        public void Delete<TEntity>(TEntity item) where TEntity : class
        {
            try
            {
                _db.Set<TEntity>().Remove(item);
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _db.SaveChangesAsync() >= 0;
            }
            catch
            {

                return false;
            }

    }

        public void Update<TEntity>(TEntity item) where TEntity : class
        {
            try
            {
                var entity = _db.Find<TEntity>(item.GetType().GetProperty("Id").GetValue(item));
                if (entity != null) _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                _db.Set<TEntity>().Update(item);
            }
            catch
            {
                throw;
            }
        }
    }

    


}
