using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Order")]
    public class Order : Entity<int>
    {

        //[Key]
        //public int Order_Id { get; set; }

        public DateTime? ReplishementExact { get; set; }
        public DateTime? AtmRepDate { get; set; }
        public DateTime? AtmExhaustionDate { get; set; }
        public Double? AtmAmount { get; set; }
        public bool? SendAlert { get; set; }
        public statut Orderstatut { get; set; }

        public Double? ActualAmount { get; set; }

        public int? Branch_Id { get; set; }
        [ForeignKey("Branch_Id")]
        public virtual Branch Branch { get; set; }
        //public string Atm_Id { get; set; }
        //[ForeignKey("Atm_Id")]
        //public virtual Atm Atm { get; set; }

        public int? CashPoint_Id { get; set; }
        [ForeignKey("CashPoint_Id")]
        public virtual CashPoint CashPoint { get; set; }

        public int CashProvider_Id { get; set; }
        [ForeignKey("CashProvider_Id")]
        public virtual CashProvider CashProvider { get; set; }

        //ajouterforeig key avec cachetock
        public int? CashStock_Id { get; set; }
        [ForeignKey("CashStock_Id")]
        public virtual CashStock CashStock { get; set; }

        //amina

        public int? OrderCategory_Id { get; set; }

        [ForeignKey("OrderCategory_Id")]
        public virtual OrderCategory OrderCategory { get; set; }

        public int? OrderType_Id { get; set; }

        [ForeignKey("OrderType_Id")]
        public virtual OrderType OrderType { get; set; }

        public nature Ordernature { get; set; }


    }
}
public enum statut
{
    Created,//0
    Validated,//1
    Pending,//2
    Canceled,//3
    Closed //4
}
public enum nature
{
    automatique,//0
    urgente,//1
    manuel,//2

}
