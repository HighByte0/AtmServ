using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ContactService : EntityService<Contact, int>, IContactService
    {
        IUnitOfWork _unitOfWork;
        IContactRepository _contactRepository;

        public ContactService(IUnitOfWork unitOfWork, IContactRepository contactRepository)
            : base(unitOfWork, contactRepository)
        {
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
