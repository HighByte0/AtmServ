using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class TempAlimRepository : GenericRepository<TempAlim, int>, ITempAlimRepository
    {
        public TempAlimRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
