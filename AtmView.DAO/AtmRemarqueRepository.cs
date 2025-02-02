using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class AtmRemarqueRepository : GenericRepository<AtmRemarque, int>, IAtmRemarqueRepository
    {
        public AtmRemarqueRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
