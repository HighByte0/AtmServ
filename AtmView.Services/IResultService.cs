using AtmView.Entities;

namespace AtmView.Services
{
    public interface IResultService : IEntityService<Result, int>
    {
        void Commit();
    }
}
