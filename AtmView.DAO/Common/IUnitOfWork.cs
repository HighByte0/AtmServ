using System;

namespace AtmView.DAO.Common
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
        System.Data.Entity.DbContext GetdbContext();
    }
}
