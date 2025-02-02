using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AtmCommError")]
    public class AtmCommError : Entity<int>
    {
        public string Atm_Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int LastState_Id { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [ForeignKey("LastState_Id")]
        public virtual State State { get; set; }

    }
}