using AtmView.Entities;

namespace AtmView.Services
{
    public interface IFactorService : IEntityService<Factor, int>
    {
        void Commit();
    }
}
