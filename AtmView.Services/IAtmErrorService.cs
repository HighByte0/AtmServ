using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAtmErrorService : IEntityService<AtmError, int>
    {
        void Commit();
    }
}
