using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{


    public class JobRepository : GenericRepository<Job, int>, IJobRepository
    {
        public JobRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

