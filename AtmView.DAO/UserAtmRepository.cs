using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class UserAtmRepository : GenericRepo<UserAtm>, IUserAtmRepository
    {
        public UserAtmRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
