using AtmView.Entities;

namespace AtmView.Services
{
    public interface IRecentAtmStateService : IEntityService<RecentAtmState, int>
    {
        void Commit();
        int getStateTypeCode(RecentAtmState recentatmState);
    }
}
