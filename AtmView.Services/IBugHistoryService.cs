using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugHistoryService : IEntityService<BugHistory, int>
    {
        void Commit();
        void Remove(BugHistory bugHistory);
    }
}
