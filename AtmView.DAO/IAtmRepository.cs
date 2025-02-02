using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public interface IAtmRepository : IGenericRepository<Atm, string>
    {
        void DeleteAtm(string atmId, string ConnectionString);
    }
}
