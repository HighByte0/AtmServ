using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("RecoTransactionType")]
    public class RecoTransactionType
    {
        [Key]
        public int Id { get; set; }


        [StringLength(50)]
        public string TransactionCode { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Label { get; set; }

        [StringLength(250)]
        public string Label_fr { get; set; }

        [StringLength(50)]
        public string Color { get; set; }
    }
}
