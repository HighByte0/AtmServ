using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class HolydayRepository : GenericRepository<Holyday, int>, IHolydayRepository
    {
        public HolydayRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
