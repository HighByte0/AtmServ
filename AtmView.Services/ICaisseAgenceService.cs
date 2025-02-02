using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICaisseAgenceService : IEntityService<CaisseAgence, int>
    {
        void Commit();

    }
}
