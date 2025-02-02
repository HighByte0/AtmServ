using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class JournalEntryMontlyService : EntiteService<JournalEntryMontly>, IJournalEntryMontlyService //EntityService<JournalEntry, int>, IJournalEntryService
    {
        IUnitOfWork _unitOfWork;
        IJournalEntryMontlyRepository _journalEntryRepository;

        public JournalEntryMontlyService(IUnitOfWork unitOfWork, IJournalEntryMontlyRepository journalEntryRepository)
            : base(unitOfWork, journalEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _journalEntryRepository = journalEntryRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
