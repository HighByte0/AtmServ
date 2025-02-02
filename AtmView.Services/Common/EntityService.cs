using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace AtmView.Services
{
    public abstract class EntityService<T, TKey> : IEntityService<T, TKey>
        where T : Entity<TKey>
        where TKey : IComparable<TKey>
    {
        IUnitOfWork _unitOfWork;
        IGenericRepository<T, TKey> _repository;

        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T, TKey> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public virtual TKey Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _repository.Save();
            _unitOfWork.Commit();
            TKey insertedId = entity.Id;
            return insertedId;
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Save();
            _repository.Edit(entity);
            _repository.Save();
            _unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            // Attacher manuellement l'entité au contexte

            _repository.Delete(entity);
            _repository.Save();
            _unitOfWork.Commit();
            
        }

        public virtual IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null)
        {
            return _repository.GetAll(predicate, includeProperties);
        }
        public virtual IEnumerable<T> GetAllRead(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = null)
        {
            return _repository.GetAllRead(predicate, includeProperties);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual IEnumerable<T> GetAllRead()
        {
            return _repository.GetAllRead();
        }


        public T GetById(TKey Id)
        {
            return _repository.GetById(Id);
        }

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

        public void RunTransaction(Action action)
        {
            _repository.RunTransaction(action);
        }

        //lkh:add access to dbctx
        public DbContext GetdbContext()
        {
            return _unitOfWork.GetdbContext();
        }

    }
}
