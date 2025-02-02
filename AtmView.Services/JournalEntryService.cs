using AtmView.Common;
using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public class JournalEntryService : EntiteService<JournalEntry>, IJournalEntryService //EntityService<JournalEntry, int>, IJournalEntryService
    {
        IUnitOfWork _unitOfWork;
        IJournalEntryRepository _journalEntryRepository;

        public JournalEntryService(IUnitOfWork unitOfWork, IJournalEntryRepository journalEntryRepository)
            : base(unitOfWork, journalEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _journalEntryRepository = journalEntryRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public List<JournalEntry> GetJournalEntryData(JournalEntyRequest request, string connectionString)
        {
            return new JournalEntryRepo().GetJournalEntryData(request, connectionString);
        }


        public List<JournalEntry> GetJournalEntryDataArchive(JournalEntyRequest request, string connectionString)
        {
            return new JournalEntryRepo().GetJournalEntryDataArchive(request, connectionString);
        }

        public int InsertJournalEntryArchive(AtmView.Entities.JournalEntry journalEntry, string connectionString)
        {
            return new JournalEntryRepo().InsertJournalEntryArchive(journalEntry, connectionString);

        }




    }
}
