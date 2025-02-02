using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugAttachmentService : EntityService<BugAttachment, int>, IBugAttachmentService
    {
        IUnitOfWork _unitOfWork;
        IBugAttachmentRepository _bugAttachmentRepository;

        public BugAttachmentService(IUnitOfWork unitOfWork, IBugAttachmentRepository bugAttachmentRepository)
            : base(unitOfWork, bugAttachmentRepository)
        {
            _unitOfWork = unitOfWork;
            _bugAttachmentRepository = bugAttachmentRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Remove(BugAttachment bugAttachment)
        {
            _bugAttachmentRepository.Delete(bugAttachment);

        }
    }
}
