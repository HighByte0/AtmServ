using AtmView.Common;
using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public interface IJournalEntryService : IEntiteService<JournalEntry>// IEntityService<JournalEntry, int>
    {

        List<JournalEntry> GetJournalEntryData(JournalEntyRequest request, string connectionString);
        List<JournalEntry> GetJournalEntryDataArchive(JournalEntyRequest request, string connectionString);

        int InsertJournalEntryArchive(AtmView.Entities.JournalEntry journalEntry, string connectionString);
    }
}
