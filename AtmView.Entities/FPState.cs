using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("FPState")]
    public class FPState:Entite
    {
        public string Task { get; set; }
        public bool IsDone { get; set; }
        [Key]
        public DateTime Time { get; set; }
        public string Result { get; set; }
    }
}
