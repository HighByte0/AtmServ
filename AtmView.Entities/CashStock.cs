using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CashStock")]
    public class CashStock : Entity<int>
    {

        public int? State_Id { get; set; }
        [ForeignKey("State_Id")]
        public virtual State State { get; set; }

        public int? Retract { get; set; }

        public int? Total { get; set; }

        public int? Treshold { get; set; }

        public virtual ICollection<CassetteStock> CassetteStocks { get; set; }

        //public int? Order_Id { get; set; }
        //[ForeignKey("Order_Id ")]
        //public virtual Order order { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
