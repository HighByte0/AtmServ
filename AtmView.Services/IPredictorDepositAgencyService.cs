using AtmView.Entities;

namespace AtmView.Services
{
    public interface IPredictorDepositAgencyService : IEntityService<PredictorDepositAgency, int>
    {
        void Commit();
    }
}
