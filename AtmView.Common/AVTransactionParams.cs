using System;

namespace AtmView.Common
{
    public class AVTransactionParams
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AtmId { get; set; }

        public DateTime TransactionDate { get; set; }
        public int TransactionNumber { get; set; }

        public int Statut { get; set; }
        public bool ExistInHost { get; set; }
        public string AutorisationNumber { get; set; }
    }
}
