using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Pr_LastDailyTransactionError")]
    public class Pr_LastDailyTransactionError : Entity<int>
    {

        public String AtmID { get; set; }

        [DisplayFormat(DataFormatString = "{0:s}",
               ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        public int TransactionNumber { get; set; }
        public int EventType_Id { get; set; }
        [ForeignKey("EventType_Id")]
        public virtual Pr_EventsType Pr_EventsType { get; set; }
        public DateTime EventTime { get; set; }
        public string Content { get; set; }

        public int duration { get; set; }

        public int ErrorType_Id { get; set; }
        [ForeignKey("ErrorType_Id")]
        public virtual Pr_EventsErrorType Pr_EventsErrorType { get; set; }
    }
}
