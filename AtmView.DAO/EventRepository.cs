using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class EventRepository : GenericRepository<Event, int>, IEventRepository
    {
        public EventRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
