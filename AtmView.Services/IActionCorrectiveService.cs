using AtmView.Entities;

namespace AtmView.Services
{
    public interface IActionCorrectiveService : IEntityService<ActionCorrective, int>
    {
        void Commit();
    }
}
