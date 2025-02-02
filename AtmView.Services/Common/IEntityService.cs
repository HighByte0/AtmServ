using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace AtmView.Services
{
    public interface IEntityService<T, TKey>
      where T : Entity<TKey>
        where TKey : IComparable<TKey>
    {

        TKey Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null);

        IEnumerable<T> GetAllRead();
        IEnumerable<T> GetAllRead(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null);
        void Update(T entity);
        IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        T GetById(TKey Id);

        void RunTransaction(Action action);


        int Count();
        int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        //lkh:add access to dbctx
        DbContext GetdbContext();
    }
}
