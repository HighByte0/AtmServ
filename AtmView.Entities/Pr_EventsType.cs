using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Pr_EventsType")]
    public class Pr_EventsType : Entity<int>
    {

        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(250)]
        public string Label { get; set; }

        [MaxLength(50)]
        public string Highlight { get; set; }

        [MaxLength(250)]
        public string Highlight_Fr { get; set; }

        
        public double Duration { get; set; }

        //public int? ErrorType_Id { get; set; }
        //[ForeignKey("ErrorType_Id")]
        //public virtual Pr_EventsErrorType Pr_EventsErrorType { get; set; }
    }
}
