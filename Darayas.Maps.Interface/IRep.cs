using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darayas.Maps.Interface
{
    public interface IRep<T> : IDisposable
        where T : class, IEntity
    {
        public IQueryable<T> Get();
        public Task<T> GetById(string Id);
        public Task<bool> AddAsync(T Entity, bool AutoSave);
        public Task<bool> EditAsync(T NewEntity, bool AutoSave);
        public Task<bool> RemoveAsync(string Id, bool AutoSave);
        public Task<int> SaveChangeAsync();
    }
}
