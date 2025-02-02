using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class AtmRejectStatutRepository : GenericRepository<AtmRejectStatut, int>, IAtmRejectStatutRepository
    {
        public AtmRejectStatutRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
