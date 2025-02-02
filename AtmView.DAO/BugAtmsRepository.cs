
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{


    public class BugAtmsRepository : GenericRepo<BugAtm>, IBugAtmsRepository
    {
        public BugAtmsRepository(AtmViewContext context)
            : base(context)
        {

        }
    }


}