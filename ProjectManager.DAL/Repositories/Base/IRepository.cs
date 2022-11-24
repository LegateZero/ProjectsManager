using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ProjectsManager.DAL.Entities.Base;

namespace ProjectsManager.DAL.Repositories.Base
{
    public interface IRepository<T> where T : Entity, new()
    {
	    public IEnumerable<T> GetItems(Expression<Func<T, bool>>? filter = null,
		    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, SortOrder order = SortOrder.Unspecified);
	    public T? Get(int id);

        public Task<T?> GetAsync(int id, CancellationToken cancel = default);

        public T Add(T item);

        public Task<T> AddAsync(T item, CancellationToken cancel = default);

        public T Update(T item);

        public Task<T> UpdateAsync(T item, CancellationToken cancel = default);

        public void Remove(int id);

        public Task RemoveAsync(int id, CancellationToken cancel = default);

    }
}
