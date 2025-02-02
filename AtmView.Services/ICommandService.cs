using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICommandService : IEntityService<Command, int>
    {

        void Commit();
    }
}
