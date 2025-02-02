using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugPriorityService : IEntityService<BugPriority, int>
    {
        void Commit();
    }
}
