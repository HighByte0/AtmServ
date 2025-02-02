using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAVTransactionAgencyService : IEntiteService<AVTransactionAgency>
    {
        void Commit();
    }
}
