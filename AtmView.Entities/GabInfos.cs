using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    public class GabInfos
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }//Identifier
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int countTrx { get; set; }
        public double MontantTotal { get; set; }
        public int AtmViewTransactions { get; set; }

    }
}
