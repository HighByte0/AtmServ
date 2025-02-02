using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("PredictorDepositAgency")]
    public class PredictorDepositAgency : Entity<int>
    {

        public Double? DepositPredicted { get; set; }
        public Double? DepositHier { get; set; }
        public Double? MSemaineDernier { get; set; }
        public Double? MSemaine7 { get; set; }
        public Int64? ConsoMmJrAnP { get; set; }
        public Int64? ConsoMmJrMP { get; set; }
        public Int64? ConsoMMJrSmDer { get; set; }
        public Double? MoyenneMoisPrec { get; set; }
        public Double? MoyenneMMSAnPrec { get; set; }
        public Double? MoyenneMMmAnPrec { get; set; }
        public Int64? DepositMaxMDer { get; set; }
        public Int64? DepositReal { get; set; }
        public int PidTotal { get; set; }
        public int Bais { get; set; }

        [Index("FactorsIndex", 3, IsUnique = true)]
        public DateTime TransactionDay { get; set; }
        [Index("FactorsIndex", 2, IsUnique = true)]
        public int Agency_Id { get; set; }
        [ForeignKey("Agency_Id")]
        public virtual Branch Branch { get; set; }


    }
}
