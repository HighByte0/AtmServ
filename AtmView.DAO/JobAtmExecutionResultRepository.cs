using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{


    public class JobAtmExecutionResultRepository : GenericRepository<JobAtmExecutionResult, int>, IJobAtmExecutionResultRepository
    {
        public JobAtmExecutionResultRepository(AtmViewContext context)
            : base(context)
        {

        }
    }


}

