using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class AddressRepository : GenericRepository<Address, int>, IAddressRepository
    {
        public AddressRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
