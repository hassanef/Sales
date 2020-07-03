
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Z.EntityFramework.Plus;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Sales.Domain.IRepositories;

namespace Sales.Infrastructura.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region [Eventing Section]
        IRepository<T> implementation;
    
   

        #endregion
        protected readonly DbContext _dbContext;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public Repository(DbContext dbContext, IHttpContextAccessor httpContextAccessor = null)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;

        }

        public virtual void Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this.SaveChanges();

        }

        public virtual async Task<T> CreateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

        
            await _dbContext.Set<T>().AddAsync(item);

            await this.SaveChangesAsync();
            //await _dbContext.SaveChangesAsync();

            return item;

        }

        public virtual async Task<T> CreateAsyncUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            await _dbContext.Set<T>().AddAsync(item);

            return item;
        }

        /// <summary>
        /// It doesn't support Creating Events. Instead use Create(T model)
        /// </summary>
        /// <param name="items"></param>
        public virtual void CreateMulti(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().AddRange(items);
            this.SaveChanges();;
        }

        public virtual async Task<bool> CreateMultiAsync(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            if (!items.Any())
                throw new ArgumentNullException("item");

            await _dbContext.Set<T>().AddRangeAsync(items);
            await this.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> CreateMultiAsyncUoW(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            if (!items.Any())
                throw new ArgumentNullException("item");

            await _dbContext.Set<T>().AddRangeAsync(items);

            return true;
        }

        public virtual void Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Entry(item).State = EntityState.Modified;

            this.SaveChanges();

        }
        public virtual async Task<T> UpdateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Entry(item).State = EntityState.Modified;

            await this.SaveChangesAsync();

            return item;
        }

        public virtual T UpdateUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            // _dbContext.Set<T>().Add(item);
            _dbContext.Set<T>().Update(item);

            return item;
        }
        public virtual void SaveChanges()
        {
            //System.Diagnostics.Debugger.Break();
            SetTenantId();

            try
            {
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
            }

        }

        private void SetTenantId()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return;

            var context = _httpContextAccessor.HttpContext;
            var user = context.User;
            var claims = user.Claims.ToList();
        }

        public virtual async System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            //System.Diagnostics.Debugger.Break();

            SetTenantId();

            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// It doesn't support deleting events. Instead Use Delete(T item)
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            _dbContext.Set<T>().Where(predicate).Delete();
            this.SaveChanges();;
        }
        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await _dbContext.Set<T>().Where(predicate).DeleteAsync();
            return await this.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsyncUoW(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).DeleteAsync();
        }

        public virtual bool DeleteUoW(T item)
        {
            _dbContext.Set<T>().Remove(item);
            return true;
        }
        public virtual void Delete(T item)
        {
        
            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            this.SaveChanges();

       }
        public virtual async void DeleteAsync(T item)
        {
            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            await this.SaveChangesAsync();

         }
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }
            this.SaveChanges();;

        }
        public virtual async void DeleteAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }
            await this.SaveChangesAsync();
        }
        public virtual IQueryable<T> FetchAll()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }
        public virtual IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().AsNoTracking() :
                 _dbContext.Set<T>().AsNoTracking().Where(predicate);
        }

        public virtual IQueryable<T> FetchMultiWithTracking(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>() :
                 _dbContext.Set<T>().Where(predicate);
        }

        public void RemoveEntitiesUoW<S>(IEnumerable<S> entities) where S : class
        {
            foreach (var entity in entities)
            {
                _dbContext.Remove<S>(entity);
            }

        }

        public virtual Boolean Any(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().Any(predicate);
        }

        public virtual async Task<Boolean> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().FirstOrDefault() : _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await _dbContext.Set<T>().FirstOrDefaultAsync() : await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbContext.Set<T>().FirstOrDefault(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual async Task<T> LastOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbContext.Set<T>().LastOrDefaultAsync(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual T LastOrDefault(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? _dbContext.Set<T>().LastOrDefault() : _dbContext.Set<T>().LastOrDefault(predicate);
        }

        public virtual async Task<T> FirstOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? await _dbContext.Set<T>().SingleOrDefaultAsync() : await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ?
            _dbContext.Set<T>().Count() :
            _dbContext.Set<T>().Count(predicate);

        }
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await _dbContext.Set<T>().CountAsync() : await _dbContext.Set<T>().CountAsync(predicate);
        }
        public virtual int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate)
        {
            return _dbContext.Set<T>().Where(predicate).Update(updatePredicate);
        }
        public virtual async Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate)
        {
            return await _dbContext.Set<T>().Where(predicate).UpdateAsync(updatePredicate);
        }
        public void ExecuteSqlQuery(SqlCommand sqlCommand)
        {
            var entityConnection = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection con = new SqlConnection(entityConnection))
            {
                using (SqlCommand cmd = sqlCommand)
                {
                    try
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public DataSet ExecuteSqlQueryWithResult(SqlCommand sqlCommand)
        {
            DataSet ds = new DataSet();
            var entityConnection = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection con = new SqlConnection(entityConnection))
            {
                using (SqlCommand cmd = sqlCommand)
                {
                    try
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds;
        }
        public T FetchFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        public async Task<T> FetchFirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public void UpdateWithAttachUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }
        public void UpdateWithAttach(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            this.SaveChanges();;
        }

        public async void UpdateWithAttachAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            await this.SaveChangesAsync();
        }

        public int RunQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<S> RunQuery<S>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public S RunRawQuery<S>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task StartTransaction(Func<Task> action)
        {

            var strategy = _dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Achieving atomicity between original Catalog database operation and the
                // IntegrationEventLog thanks to a local transaction

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    await action();
                    transaction.Commit();

                }

            });


        }

    }
}
