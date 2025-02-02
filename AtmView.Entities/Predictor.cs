using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Predictor")]
    public class Predictor : Entity<int>
    {

        public Double? SumRetraitPredicted { get; set; }
        public Double? ConsommationHier { get; set; }
        public Double? MSemaineDernier { get; set; }
        public Double? MSemaine7 { get; set; }
        public Int64? ConsoMmJrAnP { get; set; }
        public Int64? ConsoMmJrMP { get; set; }
        public Int64? ConsoMMJrSmDer { get; set; }
        public Double? MoyenneMoisPrec { get; set; }
        public Double? MoyenneMMSAnPrec { get; set; }
        public Double? MoyenneMMmAnPrec { get; set; }
        public Int64? ConsommationMaxMDer { get; set; }
        public Int64? ConsommationMaxSDer { get; set; }
        public Int64? SumRetraitReal { get; set; }
        public double PidTotal { get; set; }
        public int Bais { get; set; }
        public Boolean isFree { get; set; }



        [Index("PredictorIndex", 3, IsUnique = true)]
        public DateTime TransactionDay { get; set; }
        [Index("PredictorIndex", 2, IsUnique = true)]
        public String Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }


    }
}
