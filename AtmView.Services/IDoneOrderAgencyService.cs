using AtmView.Entities;

namespace AtmView.Services
{
    public interface IDoneOrderAgencyService : IEntityService<DoneOrderAgency, int>
    {
        void Commit();
    }
}
