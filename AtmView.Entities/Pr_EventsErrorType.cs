using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AtmView.Entities
{
    [Table("Pr_EventsErrorType")]
    public class Pr_EventsErrorType : Entity<int>
    {
        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(250)]
        public string Label { get; set; }

        [MaxLength(50)]
        public string Highlight { get; set; }

        [MaxLength(250)]
        public string Highlight_Fr { get; set; }

     
    }
}
