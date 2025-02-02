using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    public class ParsingErrors
    {
        [Key]
        public int ID { get; set; }
        public string ErrorName { get; set; }
        public string ErrorDescription { get; set; }
        public int ErrorLigne { get; set; }

    }
}
