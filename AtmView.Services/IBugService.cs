using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugService : IEntityService<Bug, int>
    {
        void Commit();
    }
}
