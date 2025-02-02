using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAtmRemarqueService : IEntityService<AtmRemarque, int>
    {
        void Commit();
    }
}
