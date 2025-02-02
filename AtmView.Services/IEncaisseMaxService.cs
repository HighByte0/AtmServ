using AtmView.Entities;

namespace AtmView.Services
{
    public interface IEncaisseMaxService : IEntityService<EncaisseMax, int>
    {
        void Commit();
    }
}
