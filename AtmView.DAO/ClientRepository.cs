using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ClientRepository : GenericRepository<Client, int>, IClientRepository
    {
        public ClientRepository(AtmViewContext context)
    : base(context)
        {

        }

    }
}
