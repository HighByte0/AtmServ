using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AtmView.Entities
{
    [Table("Pr_TransactionEvents")]
    public class Pr_TransactionEvents : Entity<int>
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

        public statutTrx statutTrx { get; set; }

    }
    public enum statutTrx
    {
        OK,//0
        ERROR,//1
    }
}
