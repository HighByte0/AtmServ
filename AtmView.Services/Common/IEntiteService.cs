using AtmView.Entities;
using System;
using System.Collections.Generic;

namespace AtmView.Services
{
    public interface IEntiteService<T>
      where T : Entite

    {

        void Create(T entity);
        void CreateOrUpdate(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool asTrack = false);
        void Update(T entity);
        IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        //T GetById(TKey Id);

        // void RunTransaction(Action action);


        int Count();
        int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate);


    }
}
