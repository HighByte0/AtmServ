using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AtmView.Entities
{
    [Table("RecoParsingResultFile")]
    public class RecoFileInfo
    {

        [Key]

        public int ID { get; set; }



        public string FileName { get; set; }


        public string HashCode { get; set; }

        public DateTime ParsingDate { get; set; }
        public string ParsingMethode { get; set; }
        public int ParsingVersion { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long NbrTransactions { get; set; }
        public long MontantTotal { get; set; }
        public Int64 NbrGAB { get; set; }
        public string FileType { get; set; }
        public long NbrLignes { get; set; }
        public string FileStatus { get; set; }

        
        public string FileErrors { get; set; }


    }
}
