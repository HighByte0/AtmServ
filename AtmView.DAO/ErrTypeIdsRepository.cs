
using AtmView.DAO.Common;
using AtmView.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AtmView.DAO
{
    public class ErrTypeIdsRepository : GenericRepository<ErrTypeId, int>, IErrTypeIdsRepository
    {
        public ErrTypeIdsRepository(AtmViewContext context)
            : base(context)
        {
           
        }
        //public List<ErrTypeId> GetreadOnly(int stateId)
        //{
        //    List<ErrTypeId> ErrTypeIds = context.ErrTypeIds.AsNoTracking().Where(i => i.State_Id == stateId).ToList();
        //    return ErrTypeIds;
        //}
    }
}
