using AtmView.Entities;

namespace AtmView.Services
{
    public interface IClientService : IEntityService<Client, int>
    {
        void Commit();
    }
}
