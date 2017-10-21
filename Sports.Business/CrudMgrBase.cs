using System;
using System.Collections.Generic;
using System.Data.Entity;
using Sports.DataAccess;

namespace Sports.Business
{
    public interface ICrudMgr<T>
    {
        IEnumerable<T> GetItems();

        T GetItem(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
    }

    public abstract class CrudMgrBase<T> : ICrudMgr<T>, IDisposable where T : class
    {
        protected readonly SportDataContext Context = new SportDataContext();
        public void Dispose()
        {
            Context?.Dispose();
        }

        protected abstract string EntityName { get; }

        protected abstract IEnumerable<T> GetEntries();

        protected abstract T GetEntry(int id);

        protected abstract void AddItem(T item);

        protected virtual void UpdateItem(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        protected abstract void DeleteItem(T item);

        public IEnumerable<T> GetItems()
        {
            try
            {
                return GetEntries();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("get entries of {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public T GetItem(int id)
        {
            try
            {
                return GetEntry(id);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("get entry of {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public void Add(T item)
        {
            try
            {
                AddItem(item);
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("add {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public void Update(T item)
        {
            try
            {
                UpdateItem(item);
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("update {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public void Delete(int id)
        {
            try
            {
                var item = GetEntry(id);
                if (item == null)
                {
                    throw new KeyNotFoundException(string.Format("Could not found id {0} in {1}", id, EntityName));
                }
                DeleteItem(item);
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("delete {0} failed, {1}", EntityName, exception.Message));
            }
        }
    }
}