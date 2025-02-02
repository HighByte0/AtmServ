using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AtmCashAlert")]
    public class AtmCashAlert : Entity<int>
    {
        public string Atm_Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int State_Id { get; set; }

        public bool Exaustion { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }

        [ForeignKey("State_Id")]
        public virtual State State { get; set; }

    }
}