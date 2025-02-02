using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    public class Im_Move_Inventory
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(50)]
        public string PreviousLocation { get; set; }

        [MaxLength(50)]
        public string NewLocation { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }


        public int AtmInventory_id { get; set; }
        [ForeignKey("AtmInventory_id")]
        public virtual Im_Atm_Inventory AtmInventory { get; set; }
    }
}
