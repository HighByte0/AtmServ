using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class JobTypeRepository : GenericRepository<JobType, int>, IJobTypeRepository
    {
        public JobTypeRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
