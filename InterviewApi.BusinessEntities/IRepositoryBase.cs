using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InterviewApi.BusinessEntities
{
    public interface IRepositoryBase : IDisposable
    {
        Task<T> InsertAsync<T>(T entity, bool saveNow = true) where T : class;

        Task<T> UpdateAsync<T>(T entity, bool saveNow = true) where T : class;
        T Update<T>(T entity, bool saveNow = true) where T : class;

        Task<T> DeleteAsync<T>(T entity, bool saveNow = true) where T : class;
        Task<T> SoftDeleteAsync<T>(T entity, bool saveNow = true) where T : class;

        Task<List<T>> DeleteRangeAsync<T>(List<T> entity, bool saveNow = true) where T : class;
        Task<List<T>> InsertRangeAsync<T>(List<T> entity, bool saveNow = true) where T : class;

        Task<bool> AnyAsyc<T>(Expression<Func<T, bool>> expression) where T : class;
        /// <summary>
        /// Generate generic query filter expression
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="expression">Filter expression</param>
        /// <returns></returns>
        IQueryable<T> GetFilter<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<int> BatchSaveAsync();
    }
}
