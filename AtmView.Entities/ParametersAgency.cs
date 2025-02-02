using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("ParametersAgency")]
    public class ParametersAgency : Entity<int>
    {
        public string name { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public string section { get; set; }
        public string type { get; set; }

        public string Agency_Id { get; set; }
        [ForeignKey("Agency_Id")]
        public virtual Agency Agency { get; set; }

        public int? CashProvider_Id { get; set; }
        [ForeignKey("CashProvider_Id")]
        public virtual CashProvider CashProvider { get; set; }
    }
}
