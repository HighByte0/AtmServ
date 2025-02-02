using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugStatutService : IEntityService<BugStatut, int>
    {
        void Commit();
    }
}
