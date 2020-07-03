using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.IRepositories
{
    public interface IDeleteRepository<T> where T : class
    {

        bool DeleteUoW(T item);
        void Delete(T item);
        void DeleteAsync(T item);
        void Delete(IEnumerable<T> entities);
        void DeleteAsync(IEnumerable<T> entities);
        Task<int> DeleteAsyncUoW(Expression<Func<T, bool>> predicate);
        void Delete(Expression<Func<T, bool>> predicate);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);

        void RemoveEntitiesUoW<S>(IEnumerable<S> entities) where S : class;


    }

}
