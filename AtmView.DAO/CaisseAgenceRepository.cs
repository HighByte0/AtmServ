using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class CaisseAgenceRepository : GenericRepository<CaisseAgence, int>, ICaisseAgenceRepository
    {
        public CaisseAgenceRepository(AtmViewContext context)
            : base(context)
        {

        }


    }
}
