using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class AVProfileRepository : GenericRepository<AtmProfile, int>, IAVProfileRepository
    {
        public AVProfileRepository(AtmViewContext context)
            : base(context)
        {

        }

        //public new void Edit(AtmProfile entity)
        //{
        //    _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //}

    }
}

