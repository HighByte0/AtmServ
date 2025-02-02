
using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAtmCashAlertsService : IEntityService<AtmCashAlert, int>
    {
        void Commit();
    }
}
