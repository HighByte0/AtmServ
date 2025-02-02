using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("ArretJourne")]
    public class ArretJourne : Entity<int>
    {

        public DateTime DateArret { get; set; }

        public Int64? SoldeOuverture { get; set; }
        public Int64? SoldeCloture { get; set; }
        public Int64? livraisons { get; set; }
        public Int64? collecte { get; set; }
        public Int64? Depot { get; set; }
        public Int64? retrait { get; set; }
        public Statuts? ArretJournestatut { get; set; }

        public int? Branch_Id { get; set; }
        [ForeignKey("Branch_Id")]
        public virtual Branch Branch { get; set; }
        public int? CashStock_Id { get; set; }
        [ForeignKey("CashStock_Id")]
        public virtual CashStock CashStock { get; set; }
    }
    public enum Statuts
    {
        Created,//0
        Validated,//1
        Pending,//2
        Canceled,//3
        Closed //4
    }

}

