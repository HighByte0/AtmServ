using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace AtmView.DAO.Common
{

    //public delegate void Del();

    public interface IGenericRepository<T, TKey>
        where T : Entity<TKey>
        where TKey : IComparable<TKey>
    {

        List<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null);
        IQueryable<T> GetAllRead(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null);

        IQueryable<T> GetAll();
        IQueryable<T> GetAllRead();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
        T GetById(TKey id);
        void RunTransaction(Action action);
        int Count();
        int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }


    //public void Exec(Action<DBTransaction> a)
    //{
    //    using (var dbTran = new DBTransaction(ConnString))
    //    {
    //        try
    //        {
    //            a(dbTran);
    //            dbTran.Commit();
    //        }
    //        catch
    //        {
    //            dbTran.Rollback();
    //            throw;
    //        }
    //    }
    //}
}
