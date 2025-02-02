using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class CommandRepository : GenericRepository<Command, int>, ICommandRepository
    {
        public CommandRepository(AtmViewContext context)
            : base(context)
        {

        }

        //public Contact GetCompleteContactById(int id)
        //{
        //    return _dbset.SingleOrDefault(x => x.Id.CompareTo(id) == 0);
        //}

    }
}

