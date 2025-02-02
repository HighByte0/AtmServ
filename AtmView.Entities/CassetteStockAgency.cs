using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("CassetteStockAgency")]
    public class CassetteStockAgency : Entity<int>
    {

       

        public int ArretJourne_Id { get; set; }
        [ForeignKey("ArretJourne_Id")]
        public virtual ArretJourne ArretJournes { get; set; }
        public int? Edition { get; set; }
        public String  currency { get; set; }
        public Double ? coupures { get; set; }
        public int? nombre { get; set; }
        public int? Total { get; set; }




    }
}
