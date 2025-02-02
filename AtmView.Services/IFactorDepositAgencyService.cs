using AtmView.Entities;

namespace AtmView.Services
{
    public interface IFactorDepositAgencyService : IEntityService<FactorDepositrAgency, int>
    {
        void Commit();
    }
}
