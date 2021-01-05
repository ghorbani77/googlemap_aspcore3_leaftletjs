using Darayas.Maps.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darayas.Maps.DAL.Repository
{
    public class SqlRepository<TEntity> : IRep<TEntity> where TEntity : class, IEntity
    {
        private MainContext db = null;
        public SqlRepository()
        {
            db = new MainContext();
            LstAdd = new List<TEntity>();
            LstDelete = new List<TEntity>();
            LstUpdate = new List<TEntity>();
        }

        private List<TEntity> LstAdd { get; set; }
        private List<TEntity> LstDelete { get; set; }
        private List<TEntity> LstUpdate { get; set; }

        public async Task<bool> AddAsync(TEntity Entity, bool AutoSave)
        {
            if (AutoSave)
            {
                await db.AddAsync(Entity);
                return await db.SaveChangesAsync() > 0;
            }
            else
            {
                LstAdd.Add(Entity);
                return true;
            }
        }

        public async Task<bool> EditAsync(TEntity Entity, bool AutoSave)
        {
            if (AutoSave)
            {
                db.Update(Entity);
                return await db.SaveChangesAsync() > 0;
            }
            else
            {
                LstUpdate.Add(Entity);
                return true;
            }
        }

        public IQueryable<TEntity> Get()
        {
            return db.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(string Id)
        {
            return await db.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<bool> RemoveAsync(string Id, bool AutoSave)
        {
            if (AutoSave)
            {
                var entity = await GetById(Id);
                db.Set<TEntity>().Remove(entity);
                return await db.SaveChangesAsync() > 0;
            }
            else
            {
                LstDelete.Add(await GetById(Id));
                return true;
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            if (LstAdd.Count() > 0)
            {
                await db.AddRangeAsync(LstAdd);
                LstAdd = new List<TEntity>();

                return await db.SaveChangesAsync();
            }

            if (LstDelete.Count() > 0)
            {
                db.RemoveRange(LstDelete);
                LstDelete = new List<TEntity>();

                return await db.SaveChangesAsync();
            }

            if (LstUpdate.Count() > 0)
            {
                db.UpdateRange(LstUpdate);
                LstUpdate = new List<TEntity>();

                return await db.SaveChangesAsync();
            }

            return 0;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                LstAdd = null;
                LstDelete = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
