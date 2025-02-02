using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AddressService : EntityService<Address, int>, IAddressService
    {
        IUnitOfWork _unitOfWork;
        IAddressRepository _addressRepository;

        public AddressService(IUnitOfWork unitOfWork, IAddressRepository addressRepository)
            : base(unitOfWork, addressRepository)
        {
            _unitOfWork = unitOfWork;
            _addressRepository = addressRepository;

        }

    }
}
