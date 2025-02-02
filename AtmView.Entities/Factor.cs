using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace AtmView.Entities
{
    [Table("Factor")]
    public class Factor : Entity<int>
    {

        //[Key]
        //public int Factor_Id { get; set; }

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
        public Int64? ConsommationMaxSDer { get; set; }
        //public int PoidJr { get; set; }
        //public int PoidName { get; set; }
        public double? PoidTot { get; set; }
        public int? intweekMonth { get; set; }
        public int? Bais { get; set; }
        //public String DayName { get; set; }




        [Index("FactorsIndex", 3, IsUnique = true)]
        public string Atm_Id { get; set; }
        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }
        [Index("FactorsIndex", 2, IsUnique = true)]
        public DateTime TransactionDay { get; set; }
    }
}
