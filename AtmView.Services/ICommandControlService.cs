using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICommandControlService : IEntityService<CommandControl, int>
    {

        void Commit();
    }
}
