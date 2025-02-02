using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{

    public class JobCommandExecutionResultRepository : GenericRepository<JobCommandExecutionResult, int>, IJobCommandExecutionResultRepository // GenericRepository<Contact, int>, IContactRepository
    {
        public JobCommandExecutionResultRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

