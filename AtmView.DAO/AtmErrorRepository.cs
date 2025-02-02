using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class AtmErrorRepository : GenericRepository<AtmError, int>, IAtmErrorRepository
    {
        public AtmErrorRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
