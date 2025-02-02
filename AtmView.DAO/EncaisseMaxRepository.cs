using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class EncaisseMaxRepository : GenericRepository<EncaisseMax, int>, IEncaisseMaxRepository
    {
        public EncaisseMaxRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
