using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("FactorAgency")]
    public class FactorAgency : Entity<int>
    {
        public Int64? SumRetrait { get; set; }
        public Int64? ConsommationHier { get; set; }
        public double? MSemaineDernier { get; set; }
        public double? MSemaine7 { get; set; }
        public Int64? ConsoMmJrAnP { get; set; }
        public Int64? ConsoMmJrMP { get; set; }
        public Int64? ConsoMMJrSmDer { get; set; }
        public double? MoyenneMoisPrec { get; set; }
        public double? MoyenneMMSAnPrec { get; set; }
        public double? MoyenneMMmAnPrec { get; set; }
        public Int64? ConsommationMaxMDer { get; set; }
        public int PoidJr { get; set; }
        public int PoidName { get; set; }
        public int PoidTot { get; set; }
        public int intweekMonth { get; set; }
        public int Bais { get; set; }
        public String DayName { get; set; }

        [Index("FactorsIndex", 3, IsUnique = true)]
        public DateTime TransactionDay { get; set; }
        [Index("FactorsIndex", 2, IsUnique = true)]
        public int Agency_Id { get; set; }
        [ForeignKey("Agency_Id")]
        public virtual Branch Branch { get; set; }


    }
}
