using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAtmRejectStatutService : IEntityService<AtmRejectStatut, int>
    {
        void Commit();

    }
}
