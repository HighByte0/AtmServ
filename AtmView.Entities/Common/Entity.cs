using System;

namespace AtmView.Entities
{
    //public abstract class BaseEntity { 

    //}

    public abstract class Entity<T> : IEntity<T> where T : IComparable<T>
    {
        public virtual T Id { get; set; }

    }


}
