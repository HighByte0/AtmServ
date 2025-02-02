using AtmView.Entities;

namespace AtmView.Services
{
    public interface IPredictorAgencyService : IEntityService<PredictorAgency, int>
    {
        void Commit();
    }
}
