using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("RecoFile")]
    public class RecoFile
    {
        public int ID { get; set; }
        public string ParsingMethod { get; set; }
        public string Nomenclature { get; set; }
        public string Extension { get; set; }
        public string DetailListOperations { get; set; }
    }
}
