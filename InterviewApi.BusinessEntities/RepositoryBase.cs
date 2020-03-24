using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static InterviewApi.Common.Extensions.ObjectExtensions;
using Microsoft.EntityFrameworkCore;

namespace InterviewApi.BusinessEntities
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        protected RepositoryBase(InterviewApiContext context)
        {
            Context = context;
        }


        protected InterviewApiContext Context { get; private set; }

        public virtual async Task<T> InsertAsync<T>(T entity, bool saveNow = true) where T : class
        {
            return await EntityManipulationAsync(entity, saveNow, EntityState.Added);
        }

        private async Task<T> EntityManipulationAsync<T>(T entity, bool saveNow, EntityState stat) where T : class
        {
            Context.Entry(entity).State = stat;
            if (saveNow) await Context.SaveChangesAsync();
            return entity;
        }

        private T EntityManipulation<T>(T entity, bool saveNow, EntityState stat) where T : class
        {
            Context.Entry(entity).State = stat;
            if (saveNow) Context.SaveChanges();
            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity, bool saveNow = true) where T : class
        {
            return await EntityManipulationAsync(entity, saveNow, EntityState.Modified);
        }

        public virtual T Update<T>(T entity, bool saveNow = true) where T : class
        {
            return EntityManipulation(entity, saveNow, EntityState.Modified);
        }

        public virtual async Task<T> DeleteAsync<T>(T entity, bool saveNow = true) where T : class
        {
            return await EntityManipulationAsync(entity, saveNow, EntityState.Deleted);
        }

        public virtual async Task<T> SoftDeleteAsync<T>(T entity, bool saveNow = true) where T : class
        {
            return await EntityManipulationAsync(entity, saveNow, EntityState.Modified);
        }

        public virtual async Task<List<T>> DeleteRangeAsync<T>(List<T> entity, bool saveNow = true) where T : class
        {
            Context.RemoveRange(entity);
            if (saveNow) await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> InsertRangeAsync<T>(List<T> entity, bool saveNow = true) where T : class
        {
            Context.AddRange(entity);
            if (saveNow) await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> AnyAsyc<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await Context.Set<T>().AnyAsync(expression);
        }

        public virtual IQueryable<T> GetFilter<T>(Expression<Func<T, bool>> expression) where T : class
        {
            if (expression.IsNull()) return Context.Set<T>();

            return Context.Set<T>().Where(expression);
        }

        public virtual async Task<int> BatchSaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        ~RepositoryBase()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        private IntPtr nativeResource = Marshal.AllocHGlobal(100);

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }
        }

    }
}
