using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class LogRepository : GenericRepository<Log, int>, ILogRepository
    {
        public LogRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
