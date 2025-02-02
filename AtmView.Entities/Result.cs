using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Result")]
    public class Result : Entity<int>
    {
        public int Agency_Id { get; set; }
        [ForeignKey("Agency_Id")]
        public virtual Branch Branch { get; set; }
        public DateTime Date { get; set; }
        public Int64? RetraitPredit { get; set; }
        public Int64? DepotPredit { get; set; }
        public Int64? AlimentationPredite { get; set; }
        public Int64? CollectePredite { get; set; }
        public Int64? SoldeFinJourneePredit { get; set; }



    }
}
