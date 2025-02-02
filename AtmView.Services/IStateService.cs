using AtmView.Entities;

namespace AtmView.Services
{
    public interface IStateService : IEntityService<State, int>
    {
        void Commit();

        void Add(State state);
    }
}
