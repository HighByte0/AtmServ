
using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugAtmsService : IEntiteService<BugAtm>
    {
        void Commit();
    }
}
