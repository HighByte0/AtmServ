using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAVProfileService : IEntityService<AtmProfile, int>
    {
        void Commit();
    }
}
