using AtmView.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AtmView.DAO.Common
{
    public abstract class GenericRepo<T> : IGenericRepo<T>
       where T : Entite //Entity<string>
                        // where TK : IComparable<TK>
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepo(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool noTrack = false)
        {
            if (noTrack)
                return _dbset.Where(predicate).AsNoTracking().AsQueryable();
            return _dbset.Where(predicate).AsQueryable();
        }
        public IQueryable<T> GetAllRead(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbset.AsNoTracking().Where(predicate).AsQueryable();
        }
        public IQueryable<T> GetAll()
        {
            return _dbset.AsQueryable();
        }
        public IQueryable<T> GetAllRead()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }


        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return GetAll(predicate);
        }

        public T Add(T entity)
        {
            return _dbset.Add(entity); ;
        }
        
        public T AddOrUpdate(T entity)
        {
            _dbset.AddOrUpdate(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            var entry = _entities.Entry(entity);
            if (entry.State == EntityState.Detached)
                _dbset.Attach(entity);
            return _dbset.Remove(entity);
        }

        public void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;

        }

        //public T GetById(TK id)
        //{
        //    return _dbset.SingleOrDefault(x => x.Id.CompareTo(id) == 0);
        //}

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public int Count()
        {
            return _dbset.Count();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbset.Count(predicate);
        }

        public void RunTransaction(Action action)
        {
            using (var dbContextTransaction = _entities.Database.BeginTransaction())
            {
                try
                {
                    action();

                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}
