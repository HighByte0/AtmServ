using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("OrderAgency")]
    public class OrderAgency : Entity<int>
    {

        //[Key]
        //public int Order_Id { get; set; }

        public DateTime Date { get; set; }
        public Double? Amount { get; set; }
        public Double? ActualAmount { get; set; }
        public statut Orderstatut { get; set; }
        public String type { get; set; }

        public string Agency_Id { get; set; }
        [ForeignKey("Agency_Id")]
        public virtual Agency Agency { get; set; }

        public int CashProvider_Id { get; set; }
        [ForeignKey("CashProvider_Id")]
        public virtual CashProvider CashProvider { get; set; }
        //ajouterforeig key avec cachetock
        public int? CashStock_Id { get; set; }
        [ForeignKey("CashStock_Id")]
        public virtual CashStockAgency CashStock { get; set; }

        public int? OrderCategory_Id { get; set; }

        [ForeignKey("OrderCategory_Id")]
        public virtual OrderCategory OrderCategory { get; set; }

        public int? OrderType_Id { get; set; }

        [ForeignKey("OrderType_Id")]
        public virtual OrderType OrderType { get; set; }

        public nature Ordernature { get; set; }
    }
}
