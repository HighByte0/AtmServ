using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{

    public class JobCommandRepository : GenericRepo<JobCommand>, IJobCommandRepository
    {
        public JobCommandRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

