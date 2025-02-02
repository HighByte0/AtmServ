using System;

namespace AtmView.Common
{
    public class JournalEntyRequest
    {
        public string AtmId { get; set; }
        public string UserId { get; set; }
        public int IsAdmin { get; set; }
        public int TodayJournal { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SearchText { get; set; }


    }
}
