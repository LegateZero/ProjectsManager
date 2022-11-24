using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Context;
using ProjectsManager.DAL.Entities.Base;
using System.Threading.Channels;

namespace ProjectsManager.DAL.Repositories.Base
{
    public class DbRepository<T> : IRepository<T>, IDisposable where T : Entity, new()
    {
        protected readonly ProjectsManagerDb _db;
        protected readonly DbSet<T> _set;
        protected bool Disposed = false;
        public bool AutoSaveChanges { get; set; } = true;
        public DbRepository(ProjectsManagerDb db)
        {
            _db = db;
            _set = _db.Set<T>();
        }

        protected virtual IQueryable<T> _items => _set;

        public IEnumerable<T> GetItems(Expression<Func<T, bool>>? filter = null,
	        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, SortOrder order = SortOrder.Unspecified) 
        {
	        var query = _items; 
	        if (filter != null)
		        query = query.Where(filter);
	        if (orderBy != null)
		        query = orderBy(query);
	        return query;

        }

        public virtual T? Get(int id) => _items
            .SingleOrDefault(item => item.Id == id);

        public virtual async Task<T?> GetAsync(int id, CancellationToken cancel = default) => await _items
            .SingleOrDefaultAsync(item => item.Id == id, cancel)
            .ConfigureAwait(false);

        public virtual T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _set.Add(item);
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public virtual async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var s = await _set.AddAsync(item, cancel);
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel);
            return await GetAsync(s.Entity.Id, cancel);
        }


        public virtual T Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var local = _set
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(item.Id));

            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public virtual async Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var local = _set
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(item.Id));

            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel);
            return item;
        }


        public virtual void Remove(int id)
        {
            var item = _set.Local.FirstOrDefault(item => item.Id == id)
                           ?? new T { Id = id };
            _db.Remove(item);
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public virtual async Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            var item =
                _set.Local.FirstOrDefault(item => item.Id == id)
                ?? new T { Id = id };
            _db.Remove(item);
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel);
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;
                _db.Dispose();
            }
        }
    }
}
