using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AtmArreteJoune")]
    public class AtmArreteJoune : Entity<int>
    {

        public DateTime EndDate { get; set; }
        public int Closingbalance { get; set; }
        public int Retract { get; set; }
        public int Rejected { get; set; }
        public StatutsArret? ArretJournestatut { get; set; }

        public string Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }
        public virtual ICollection<ArretCassetteStock> CassetteStocks { get; set; }

        public int? State_Id { get; set; }
        [ForeignKey("State_Id")]
        public virtual State State { get; set; }
    }
    public enum StatutsArret
    {
        OK,//0
        ERROR,//1
        
    }

}

