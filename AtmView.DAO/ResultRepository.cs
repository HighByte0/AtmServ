using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ResultRepository : GenericRepository<Result, int>, IResultRepository
    {
        public ResultRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
