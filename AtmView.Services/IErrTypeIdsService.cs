
using AtmView.Entities;

namespace AtmView.Services
{
    public interface IErrTypeIdsService : IEntityService<ErrTypeId, int>
    {
        void Commit();
    }
}
