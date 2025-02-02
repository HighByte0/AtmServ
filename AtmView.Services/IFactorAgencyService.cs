using AtmView.Entities;

namespace AtmView.Services
{
    public interface IFactorAgencyService : IEntityService<FactorAgency, int>
    {
        void Commit();
    }
}
