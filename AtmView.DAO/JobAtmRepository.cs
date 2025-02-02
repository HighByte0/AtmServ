using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{


    public class JobAtmRepository : GenericRepo<JobAtm>, IJobAtmRepository
    {
        public JobAtmRepository(AtmViewContext context)
            : base(context)
        {

        }
    }


}

