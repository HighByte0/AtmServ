using AtmView.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace AtmView.DAO.Common
{


    public interface IGenericRepo<T>
        where T : Entite//Entity<TKey>
        //where TKey : IComparable<TKey>
    {

        IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool asTrack = false);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllRead(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllRead();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T AddOrUpdate(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
        //T GetById(TKey id);
        void RunTransaction(Action action);

        int Count();
        int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }


}
