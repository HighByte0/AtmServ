using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("FrequentCommand")]
    public class FrequentCommand : Entity<int>
    {
        public string Description { get; set; }
        public string Arg1 { get; set; }
        public string Arg2 { get; set; }
        public string Arg3 { get; set; }
    }
}
