
using AtmView.Entities;

namespace AtmView.Services
{
    public interface IErrorTypeService : IEntityService<ErrorType, int>
    {
        void Commit();
    }
}
