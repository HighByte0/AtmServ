using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;

namespace AtmView.Services
{
    public abstract class EntiteService<T> : IEntiteService<T>
        where T : Entite//Entity<TKey>
        //where TKey : IComparable<TKey>
    {
        IUnitOfWork _unitOfWork;
        //IGenericRepository<T, TKey> _repository;
        IGenericRepo<T> _repository;

        public EntiteService(IUnitOfWork unitOfWork, IGenericRepo<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _repository.Add(entity);
            _repository.Save();
            _unitOfWork.Commit();
        }
        public virtual void CreateOrUpdate(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _repository.AddOrUpdate(entity);
            _unitOfWork.Commit();
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
            _unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public virtual IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool asTrack = false)
        {
            return _repository.GetAll(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }


        //public T GetById(TKey Id)
        //{
        //    return _repository.GetById(Id);
        //}

        public virtual IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _repository.GetAll(predicate);
        }


        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _repository.Count(predicate);
        }

        public int Count()
        {
            return _repository.Count();
        }

        //public void RunTransaction(Action action)
        //{
        //    _repository.RunTransaction(action);
        //}
    }
}
