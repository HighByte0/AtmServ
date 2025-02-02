using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAtmService : IEntityService<Atm, string>
    {
        void Commit();
        void DeleteAtm(string atmId, string ConnectionString);
    }
}
