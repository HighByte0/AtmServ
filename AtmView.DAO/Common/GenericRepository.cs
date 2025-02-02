using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace AtmView.DAO.Common
{
    public abstract class GenericRepository<T, TK> : IGenericRepository<T, TK>
       where T : Entity<TK>
        where TK : IComparable<TK>
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public List<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null)
        {
            IQueryable<T> query = _dbset.AsNoTracking().Where(predicate);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.AsNoTracking().ToList();
        }
        public IQueryable<T> GetAllRead(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null)
        {
            IQueryable<T> query = _dbset.AsNoTracking().Where(predicate);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.AsQueryable();
        }

        public int Count()
        {
            return _dbset.Count();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbset.Count(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }
        public IQueryable<T> GetAllRead()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }


        public List<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return GetAll(predicate);
        }

        public T Add(T entity)
        {
            return _dbset.Add(entity);
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
            //var entry = _entities.Entry(entity);
            //var aExists = _dbset.Find(entity.Id);
            var attachedEntities = _dbset.Local;
            //if(entity is RecentAtmState)
            //{
            //    var dbEntry = entry.GetDatabaseValues();
            //    var dbValues = (RecentAtmState)dbEntry.ToObject();
            //    var ra = aExists as RecentAtmState;
            //    var newRA = entity as RecentAtmState;
            //    if(dbValues.LastSeen > newRA.LastSeen)
            //    {
            //        newRA.LastSeen = ra.LastSeen;
            //        newRA.State_Id = ra.State_Id;
            //        entity = newRA as Entity;
            //        _entities.Entry(aExists).State = EntityState.Detached; _dbset.Attach(newRA);
            //        _entities.Entry(entity).State = EntityState.Modified;
            //    }
            //}
            //_entities.Entry(aExists).State = EntityState.Detached; _dbset.Attach(entity);
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public T GetById(TK id)
        {
            return _dbset/*.AsNoTracking()*/.SingleOrDefault(x => x.Id.CompareTo(id) == 0);
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
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
