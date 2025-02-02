using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class TransationTypeRepository : GenericRepository<TransationType, int>, ITransationTypeRepository
    {
        public TransationTypeRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
