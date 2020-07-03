using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.IRepositories
{
    public interface IRepository<T> : ISaveRepository<T>, IDeleteRepository<T>, IFetchRepository<T> where T : class
    {

        int RunQuery(string query, params object[] parameters);

        IList<S> RunQuery<S>(string query, params object[] parameters);

        S RunRawQuery<S>(string query, params object[] parameters);

        System.Threading.Tasks.Task<int> SaveChangesAsync();

        void SaveChanges();

        Task StartTransaction(Func<Task> action);
    }
}
