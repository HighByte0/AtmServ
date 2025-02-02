using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class CommandControlRepository : GenericRepository<CommandControl, int>, ICommandControlRepository
    {
        public CommandControlRepository(AtmViewContext context)
            : base(context)
        {

        }

        //public Contact GetCompleteContactById(int id)
        //{
        //    return _dbset.SingleOrDefault(x => x.Id.CompareTo(id) == 0);
        //}

    }
}

