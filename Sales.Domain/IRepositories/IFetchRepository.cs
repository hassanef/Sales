using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.IRepositories
{
    public interface IFetchRepository<T> where T : class
    {

        IQueryable<T> FetchAll();

        IQueryable<T> FetchMultiWithTracking(Expression<Func<T, bool>> predicate = null);

        IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null);

        Boolean Any(Expression<Func<T, bool>> predicate);

        Task<Boolean> AnyAsync(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);

        T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate);

        Task<T> LastOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate);

        T LastOrDefault(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

        T FetchFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate);

        Task<T> FetchFirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> predicate);

        void ExecuteSqlQuery(SqlCommand cmd);

        DataSet ExecuteSqlQueryWithResult(SqlCommand cmd);
    }
}
