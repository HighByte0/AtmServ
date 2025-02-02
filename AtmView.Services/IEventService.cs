using AtmView.Entities;

namespace AtmView.Services
{
    public interface IEventService : IEntityService<Event, int>
    {
        void Commit();
    }
}
