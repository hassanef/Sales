using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sales.Domain.IRepositories
{
    public interface ISaveRepository<T> where T : class
    {


        void Create(T item);

        Task<T> CreateAsync(T item);

        Task<T> CreateAsyncUoW(T item);

        Task<bool> CreateMultiAsyncUoW(IEnumerable<T> items);

        void Update(T item);

        Task<T> UpdateAsync(T item);

        T UpdateUoW(T item);
        void UpdateWithAttachUoW(T item);

        void CreateMulti(IEnumerable<T> items) ;

        Task<bool> CreateMultiAsync(IEnumerable<T> items);

        int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate);

        Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate);

        void UpdateWithAttach(T item);

        void UpdateWithAttachAsync(T item);
    }
}
