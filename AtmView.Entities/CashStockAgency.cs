using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("CashStockAgency")]
    public class CashStockAgency : Entity<int>
    {

        public int? State_Id { get; set; }
        [ForeignKey("State_Id")]
        public virtual State State { get; set; }

        public int? Retract { get; set; }

        public int? Total { get; set; }

        public int? Treshold { get; set; }

        public virtual ICollection<CassetteStockAgency> CassetteStockAgencys { get; set; }

        //public int? Order_Id { get; set; }
        //[ForeignKey("Order_Id ")]
        //public virtual Order order { get; set; }
        public virtual ICollection<OrderAgency> OrderAgencys { get; set; }


    }
}
